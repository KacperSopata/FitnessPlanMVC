using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IExerciseRepository
    {
        IQueryable<Exercise> GetAllExercises();
        Exercise GetDetail(int id);
        void AddExercise(Exercise exercise);
        Exercise GetDetailByWorkoutExercise(int id);
    }
}
