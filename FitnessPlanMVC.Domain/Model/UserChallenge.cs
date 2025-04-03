using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class UserChallenge
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }

        public int Progress { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? LastProgressDate { get; set; } // <-- ważne!
    }
}
