using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IUserChallengeRepository
    {
        IEnumerable<UserChallenge> GetByUserId(string userId);
        UserChallenge GetById(int id);
        int AssignUserToChallenge(UserChallenge userChallenge);
        void UpdateProgress(int userChallengeId, int progress);
        void MarkAsCompleted(int userChallengeId);
        bool IsUserEnrolled(string userId, int challengeId);
        UserChallenge GetByChallengeAndUser(int challengeId, string userId);
        void UpdateProgressWithDate(int userChallengeId, int progress, DateTime lastProgressDate);
    }
}
