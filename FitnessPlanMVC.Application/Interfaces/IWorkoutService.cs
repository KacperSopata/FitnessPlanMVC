using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.Workout;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IWorkoutService
    {
        void DeleteProduct(int id);
        int AddExerciseToWorkout(NewWorkoutExerciseVm exercise);
        Task<int> AddWorkout(NewWorkoutVm product);
        void DeleteWorkout(int workoutid);
        NewWorkoutExerciseVm GetWorkoutExerciseById(int id);
        List<WorkoutDetailVm> GetWorkouts(string userId, DateTime selectedDate);
        void UpdateExercise(NewWorkoutExerciseVm model);

    }
}
