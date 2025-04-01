using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Workout;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace FitnessPlanMVC.Application.Service
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepo;
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public WorkoutService(IWorkoutRepository workoutRepo, IExerciseRepository exerciseRepo, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _workoutRepo = workoutRepo;
            _exerciseRepo = exerciseRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public List<WorkoutDetailVm> GetWorkouts(string userId, DateTime selectedDate)
        {
            // Pobieramy wszystkie treningi przypisane do danego użytkownika
            var workouts = _workoutRepo.GetAllWorkouts(selectedDate, userId); // Zakładając, że GetAllWorkouts uwzględnia userId
            var workoutVms = new List<WorkoutDetailVm>();

            foreach (var workout in workouts)
            {
                var workoutVm = _mapper.Map<WorkoutDetailVm>(workout);

                // Pobieramy ćwiczenia powiązane z danym treningiem i użytkownikiem
                var exercises = _workoutRepo.GetExercises(workout.Id, userId); // Uwzględniamy userId także tutaj

                if (exercises != null && exercises.Any())
                {
                    workoutVm.Exercises = exercises
                        .Select(e => _mapper.Map<ExerciseForListVm>(e))
                        .ToList();
                }

                workoutVms.Add(workoutVm);
            }

            return workoutVms;
        }


        public int AddExerciseToWorkout(NewWorkoutExerciseVm exercise)
        {
            var exer = _mapper.Map<WorkoutExercise>(exercise);
            var id = _workoutRepo.AddExercise(exer);
            return id;
        }

        public void DeleteProduct(int id)
        {
            _workoutRepo.DeleteProduct(id);
        }


        public async Task<int> AddWorkout(NewWorkoutVm product)
        {
            var prod = _mapper.Map<Workout>(product);
            var user = await _userManager.FindByIdAsync(product.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID.");
            }
            prod.ApplicationUser = user;
            var id = _workoutRepo.AddWorkout(prod);
            return id;

        }

        public void DeleteWorkout(int workoutid)
        {
            _workoutRepo.DeleteWorkout(workoutid);
        }

        public NewWorkoutExerciseVm GetWorkoutExerciseById(int id)
        {
            var exercise = _workoutRepo.GetWorkoutExerciseById(id);
            var exerciseVm = _mapper.Map<NewWorkoutExerciseVm>(exercise);
            return exerciseVm;
        }

        public void UpdateExercise(NewWorkoutExerciseVm model)
        {
            var workoutexercise = _mapper.Map<WorkoutExercise>(model);
            _workoutRepo.UpdateProduct(workoutexercise);
        }
    }
}