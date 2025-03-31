using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public Workout Workouts { get; set; }
        public int WorkoutId { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
    }
}
