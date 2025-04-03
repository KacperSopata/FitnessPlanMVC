using AutoMapper;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Challenge;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanMVC.Application.Service
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserChallengeRepository _userChallengeRepository;
        private readonly IMapper _mapper;

        public ChallengeService(IChallengeRepository challengeRepository, IUserChallengeRepository userChallengeRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _userChallengeRepository = userChallengeRepository;
            _mapper = mapper;
        }

        public List<ChallengeForListVm> GetAllChallenges()
        {
            var challenges = _challengeRepository.GetAll();
            return _mapper.Map<List<ChallengeForListVm>>(challenges);
        }

        public int AddChallenge(NewChallengeVm model)
        {
            var challenge = _mapper.Map<Challenge>(model);
            challenge.EndDate = model.StartDate.AddDays(model.DurationInDays);
            return _challengeRepository.Add(challenge);
        }

        public ChallengeDetailVm GetChallengeById(int id)
        {
            var challenge = _challengeRepository.GetById(id);
            return _mapper.Map<ChallengeDetailVm>(challenge);
        }

        public void UpdateChallenge(NewChallengeVm model)
        {
            var challenge = _mapper.Map<Challenge>(model);
            challenge.EndDate = model.StartDate.AddDays(model.DurationInDays);
            _challengeRepository.Update(challenge);
        }

        public void DeleteChallenge(int id)
        {
            _challengeRepository.Delete(id);
        }

        public void AssignUserToChallenge(int challengeId, string userId)
        {
            if (!_userChallengeRepository.IsUserEnrolled(userId, challengeId))
            {
                var userChallenge = new UserChallenge
                {
                    ChallengeId = challengeId,
                    UserId = userId,
                    Progress = 0,
                    IsCompleted = false,
                    CompletionDate = null,
                    LastProgressDate = null
                };
                _userChallengeRepository.AssignUserToChallenge(userChallenge);
            }
        }

        public bool IsUserEnrolled(string userId, int challengeId)
        {
            return _userChallengeRepository.IsUserEnrolled(userId, challengeId);
        }

        public List<UserChallengeVm> GetUserChallenges(string userId)
        {
            var userChallenges = _userChallengeRepository.GetByUserId(userId);
            return userChallenges.Select(uc =>
            {
                bool canAdd = !uc.IsCompleted &&
                              (!uc.LastProgressDate.HasValue ||
                              (DateTime.Now - uc.LastProgressDate.Value).TotalHours >= 24);

                TimeSpan? timeLeft = uc.LastProgressDate.HasValue
                    ? uc.LastProgressDate.Value.AddHours(24) - DateTime.Now
                    : null;

                return new UserChallengeVm
                {
                    ChallengeId = uc.ChallengeId,
                    ChallengeName = uc.Challenge?.Name ?? "",
                    Progress = uc.Progress,
                    IsCompleted = uc.IsCompleted,
                    CompletionDate = uc.CompletionDate,
                    EndDate = uc.Challenge?.EndDate,
                    DurationInDays = uc.Challenge?.DurationInDays ?? 0,
                    LastProgressDate = uc.LastProgressDate,
                    CanAddProgressToday = canAdd,
                    TimeUntilNextAvailable = timeLeft
                };
            }).ToList();
        }


        public void UpdateUserProgress(int challengeId, string userId, int progressToAdd)
        {
            var userChallenge = _userChallengeRepository.GetByChallengeAndUser(challengeId, userId);
            if (userChallenge == null || userChallenge.IsCompleted)
                return;

            if (userChallenge.LastProgressDate.HasValue &&
                (DateTime.Now - userChallenge.LastProgressDate.Value).TotalHours < 24)
            {
                return;
            }

            userChallenge.Progress = Math.Min(
                userChallenge.Progress + progressToAdd,
                userChallenge.Challenge.DurationInDays
            );

            userChallenge.LastProgressDate = DateTime.Now;

            if (userChallenge.Progress >= userChallenge.Challenge.DurationInDays)
            {
                userChallenge.IsCompleted = true;
                userChallenge.CompletionDate = DateTime.Now;
                _userChallengeRepository.MarkAsCompleted(userChallenge.Id);
            }

            _userChallengeRepository.UpdateProgressWithDate(
                userChallenge.Id,
                userChallenge.Progress,
                userChallenge.LastProgressDate.Value
            );
        }

        public bool CanAddProgressToday(int challengeId, string userId)
        {
            var userChallenge = _userChallengeRepository.GetByChallengeAndUser(challengeId, userId);
            if (userChallenge == null || userChallenge.IsCompleted)
                return false;

            return userChallenge.LastProgressDate == null ||
                   (DateTime.Now - userChallenge.LastProgressDate.Value).TotalHours >= 24;
        }
    }
}
