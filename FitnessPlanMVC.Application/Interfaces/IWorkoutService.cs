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
        List<WorkoutDetailVm> GetWorkouts(DateTime selectedDate);
        void DeleteProduct(int id);
        int AddExerciseToWorkout(NewWorkoutExerciseVm exercise);
        int AddWorkout(NewWorkoutVm product);
        void DeleteWorkout(int workoutid);
        NewWorkoutExerciseVm GetWorkoutExerciseById(int id);
        void UpdateExercise(NewWorkoutExerciseVm model);

    }
}
