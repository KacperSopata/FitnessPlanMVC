using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
        public string Tips { get; set; }
        public string InvolvedParties { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercise { get; set; }
    }
}
