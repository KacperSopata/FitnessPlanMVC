using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class UserChallengeRepository : IUserChallengeRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public UserChallengeRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserChallenge> GetByUserId(string userId)
        {
            return _context.UserChallenges
                .Include(uc => uc.Challenge)
                .Where(uc => uc.UserId == userId)
                .ToList();
        }

        public UserChallenge GetById(int id)
        {
            return _context.UserChallenges
                .Include(uc => uc.Challenge)
                .FirstOrDefault(uc => uc.Id == id);
        }

        public int AssignUserToChallenge(UserChallenge userChallenge)
        {
            _context.UserChallenges.Add(userChallenge);
            _context.SaveChanges();
            return userChallenge.Id;
        }

        public void UpdateProgress(int userChallengeId, int progress)
        {
            var entity = _context.UserChallenges.FirstOrDefault(x => x.Id == userChallengeId);
            if (entity != null)
            {
                entity.Progress = progress;
                _context.SaveChanges();
            }
        }

        public void MarkAsCompleted(int userChallengeId)
        {
            var entity = _context.UserChallenges.FirstOrDefault(x => x.Id == userChallengeId);
            if (entity != null)
            {
                entity.IsCompleted = true;
                entity.CompletionDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public bool IsUserEnrolled(string userId, int challengeId)
        {
            return _context.UserChallenges.Any(x => x.UserId == userId && x.ChallengeId == challengeId);
        }
    }
}
