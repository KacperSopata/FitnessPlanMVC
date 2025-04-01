using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IWorkoutRepository
    {
        bool WorkoutExistForDate(DateTime selectedDate);
        List<WorkoutExercise> GetExercises(int workoutId, string userId);
        void AddExerciseTo(WorkoutExercise workout);
        Workout GetWorkoutById(int workoutId);
        void DeleteProduct(int id);
        void UpdateExercise(int workoutId, int exerciseId, int sets, int reps, float weight);
        int AddWorkout(Workout product);
        void DeleteWorkout(int workoutid);
        int AddExercise(WorkoutExercise exer);
        WorkoutExercise GetWorkoutExerciseById(int id);
        void UpdateProduct(WorkoutExercise workoutexercise);
        List<Workout> GetAllWorkouts(DateTime selectedDate, string userId);

    }
}
