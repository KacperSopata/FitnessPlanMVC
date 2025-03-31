using System.Linq;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class ReadyPlanWorkoutRepository : IReadyPlanWorkoutRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public ReadyPlanWorkoutRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }

        public int AddReadyPlanWorkout(ReadyPlanWorkout plan)
        {
            _context.ReadyPlanWorkouts.Add(plan);
            _context.SaveChanges();
            return plan.Id;
        }

        public void DeleteReadyPlanWorkout(ReadyPlanWorkout planWorkout)
        {
            _context.ReadyPlanWorkouts.Remove(planWorkout);
            _context.SaveChanges();
        }

        public IQueryable<ReadyPlanWorkout> GetAllPlanReadyWorkout()
        {
            return _context.ReadyPlanWorkouts;
        }

        public ReadyPlanWorkout GetDetail(int id)
        {
            return _context.ReadyPlanWorkouts
                           .FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByDifficulty(string difficulty)
        {
            return _context.ReadyPlanWorkouts
                           .Where(p => p.Difficulty == difficulty);
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByType(string type)
        {
            return _context.ReadyPlanWorkouts
                                 .Where(plan => plan.PlanType == type)
                                 .AsQueryable();
        }

        public IQueryable<ReadyPlanWorkout> GetPlansByTypeAndDifficulty(string type, string difficulty)
        {
            return _context.ReadyPlanWorkouts
                                   .Where(plan => plan.PlanType == type && plan.Difficulty == difficulty)
                                   .AsQueryable();
        }
    }
}