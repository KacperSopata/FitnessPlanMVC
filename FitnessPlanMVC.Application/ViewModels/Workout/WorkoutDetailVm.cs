using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Application.ViewModels.Workout
{
    public class WorkoutDetailVm : IMapFrom<FitnessPlanMVC.Domain.Model.Exercise>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartWorkout { get; set; }
        public List<ExerciseForListVm> Exercises { get; set; }
        public List<WorkoutDetailVm> Workouts { get; set; } // Lista treningów
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Workout, WorkoutDetailVm>().ReverseMap();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Exercise, ExerciseForListVm>();
            profile.CreateMap<WorkoutExercise, WorkoutDetailVm>().ReverseMap();
        }
    }
}
