using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public ExerciseRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }
        public IQueryable<Exercise> GetAllExercises()
        {
            return _context.Exercises.AsQueryable();
        }

        public Exercise GetDetail(int id)
        {
            return _context.Exercises.FirstOrDefault(e => e.Id == id);
        }

        public Exercise GetDetailByWorkoutExercise(int id)
        {
            var workoutExercise = _context.WorkoutExercises.FirstOrDefault(we => we.Id == id);

            return _context.Exercises.FirstOrDefault(e => e.Id == workoutExercise.ExerciseId);
        }
    }
}