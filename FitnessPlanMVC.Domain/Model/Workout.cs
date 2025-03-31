using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class Workout
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartWorkout { get; set; }
        public DateTime EndWorkout { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
