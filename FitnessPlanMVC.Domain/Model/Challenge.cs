using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public int Goal { get; set; } // np. 100 przysiadów, 10000 kcal
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<UserChallenge> UserChallenges { get; set; } = new();
    }
}
