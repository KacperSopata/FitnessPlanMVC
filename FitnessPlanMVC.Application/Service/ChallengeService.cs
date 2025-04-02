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
                    CompletionDate = null
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
            return userChallenges.Select(uc => new UserChallengeVm
            {
                ChallengeId = uc.ChallengeId,
                ChallengeName = uc.Challenge?.Name ?? "",  // Nazwa wyzwania
                Progress = uc.Progress,
                IsCompleted = uc.IsCompleted,
                CompletionDate = uc.CompletionDate,
                EndDate = uc.Challenge?.EndDate, // Dodajemy datę zakończenia wyzwania
                DurationInDays = uc.Challenge?.DurationInDays ?? 0  // Ustawienie DurationInDays z wyzwania
            }).ToList();
        }

        public void UpdateUserProgress(int challengeId, string userId, int progressToAdd)
        {
            var userChallenges = _userChallengeRepository.GetByUserId(userId);
            var userChallenge = userChallenges.FirstOrDefault(c => c.ChallengeId == challengeId);

            if (userChallenge != null && !userChallenge.IsCompleted)
            {
                userChallenge.Progress += progressToAdd;

                if (userChallenge.Progress >= userChallenge.Challenge.Goal)
                {
                    userChallenge.IsCompleted = true;
                    userChallenge.CompletionDate = DateTime.Now;
                }

                _userChallengeRepository.UpdateProgress(userChallenge.Id, userChallenge.Progress);
                if (userChallenge.IsCompleted)
                {
                    _userChallengeRepository.MarkAsCompleted(userChallenge.Id);
                }
            }
        }

    }
}
