using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.Challenge
{
    public class UserChallengeVm
    {
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? EndDate { get; set; } // Nowa właściwość na datę
        public int DurationInDays { get; set; }
        public bool CanAddProgressToday { get; set; }
        public DateTime? LastProgressDate { get; set; }
        public TimeSpan? TimeUntilNextAvailable { get; set; }

    }
}
