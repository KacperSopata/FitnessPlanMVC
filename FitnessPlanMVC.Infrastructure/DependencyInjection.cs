using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IReadyPlanWorkoutRepository, ReadyPlanWorkoutRepository>();
            services.AddTransient<IReadyRecipesRepository, ReadyRecipesRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IStandardMealRepository, StandardMealRepository>();
            services.AddTransient<IUserMealRepository, UserMealRepository>();
            services.AddTransient<IUserChallengeRepository, UserChallengeRepository>();
            services.AddTransient<IChallengeRepository, ChallengeRepository>();
            //services.AddTransient<IChallengeProgressRepository, ChallengeProgressRepository>();
            return services;
        }
    }
}
