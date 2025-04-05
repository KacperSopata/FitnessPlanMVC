using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitnessPlanMVCDbContext _context;
        public WorkoutRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }

        public bool WorkoutExistForDate(DateTime selectedDate)
        {
            return _context.Workouts.Any(m => m.StartWorkout.Date == selectedDate.Date);
        }
        public List<WorkoutExercise> GetExercises(int workoutId, string userId)
        {
            return _context.WorkoutExercises
                .Where(e => e.WorkoutId == workoutId && e.UserId == userId)
                .Select(e => new WorkoutExercise
                {
                    Id = e.Id,
                    Exercise = e.Exercise,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight
                })
                .ToList();
        }

        public void AddExerciseTo(WorkoutExercise workout)
        {
            _context.WorkoutExercises.Add(workout);
            _context.SaveChanges();
        }



        public Workout GetWorkoutById(int workoutId)
        {
            var workout = _context.Workouts.FirstOrDefault(i => i.Id == workoutId);
            return workout;
        }

        public void DeleteProduct(int id)
        {
            var exercise = _context.WorkoutExercises.FirstOrDefault(d => d.Id == id);
            if (exercise != null)
            {
                _context.WorkoutExercises.Remove(exercise);
                _context.SaveChanges();
            }
        }

        public void UpdateExercise(int workoutId, int exerciseId, int sets, int reps, float weight)
        {
            var exercise = _context.WorkoutExercises.FirstOrDefault(d => d.WorkoutId == workoutId);

            if (exercise != null)
            {
                exercise.Sets = sets;
                exercise.Reps = reps;
                exercise.Weight = weight;

                _context.Entry(exercise).Property(e => e.Sets).IsModified = true;
                _context.Entry(exercise).Property(e => e.Reps).IsModified = true;
                _context.Entry(exercise).Property(e => e.Weight).IsModified = true;
                _context.SaveChanges();
            }
        }
        public int AddWorkout(Workout product)
        {
            _context.Workouts.Add(product);
            _context.SaveChanges();
            return product.Id;

        }
        public void DeleteWorkout(int workoutid)
        {
            var workoutDetails = _context.WorkoutExercises.Where(wd => wd.WorkoutId == workoutid).ToList();
            if (workoutDetails.Any())
            {
                _context.WorkoutExercises.RemoveRange(workoutDetails);
            }
            var workout = _context.Workouts.Find(workoutid);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
            }
            _context.SaveChanges();
        }

        public int AddExercise(WorkoutExercise exer)
        {
            exer.Workouts = _context.Workouts.Include(m => m.WorkoutExercises).FirstOrDefault(m => m.Id == exer.WorkoutId);
            exer.Exercise = _context.Exercises.Find(exer.ExerciseId);
            exer.ApplicationUser = _context.ApplicationUsers.Find(exer.ApplicationUser);
            _context.WorkoutExercises.Add(exer);
            _context.SaveChanges();
            return exer.Id;
        }

        public WorkoutExercise GetWorkoutExerciseById(int id)
        {
            var exercise = _context.WorkoutExercises
                .FirstOrDefault(e => e.Id == id);

            return exercise;
        }


        public void UpdateProduct(WorkoutExercise workoutexercise)
        {
            _context.Update(workoutexercise);
            _context.Entry(workoutexercise).Property("Sets").IsModified = true;
            _context.Entry(workoutexercise).Property("Reps").IsModified = true;
            _context.Entry(workoutexercise).Property("Weight").IsModified = true;
            _context.SaveChanges();

        }
        public List<Workout> GetAllWorkouts(DateTime selectedDate, string userId)
        {
            return _context.Workouts
                           .Where(w => w.StartWorkout.Date == selectedDate.Date && w.UserId == userId)
                           .ToList();
        }
    }
}