using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.Challenge;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IChallengeService
    {
        List<ChallengeForListVm> GetAllChallenges();
        int AddChallenge(NewChallengeVm model);
        ChallengeDetailVm GetChallengeById(int id);
        void UpdateChallenge(NewChallengeVm model);
        void DeleteChallenge(int id);
        void AssignUserToChallenge(int challengeId, string userId);
        bool IsUserEnrolled(string userId, int challengeId);
        List<UserChallengeVm> GetUserChallenges(string userId);
        void UpdateUserProgress(int challengeId, string userId, int progressToAdd);
    }
}
