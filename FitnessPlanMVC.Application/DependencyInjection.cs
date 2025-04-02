using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IReadyPlanWorkoutService, ReadyPlanWorkoutService>();
            services.AddTransient<IReadyRecipesService, ReadyRecipesService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IWorkoutService, WorkoutService>();
            services.AddTransient<IStandardMealService, StandardMealService>();
            services.AddTransient<IUserMealService, UserMealService>();
            services.AddTransient<IChallengeService, ChallengeService>();
            //services.AddTransient<IChallengeProgressService, ChallengeProgressService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
