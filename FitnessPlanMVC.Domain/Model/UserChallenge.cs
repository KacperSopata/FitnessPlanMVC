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
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }
        public string UserId { get; set; }
        // Ewentualne postępy:
        public int Progress { get; set; } // np. ile przysiadów/kcal wykonano
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int DurationInDays { get; set; }  // Dodajemy właściwość DurationInDays
    }
}
