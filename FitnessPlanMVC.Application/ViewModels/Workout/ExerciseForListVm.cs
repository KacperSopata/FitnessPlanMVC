using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Workout
{
    public class ExerciseForListVm : IMapFrom<FitnessPlanMVC.Domain.Model.Exercise>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Exercise, ExerciseForListVm>();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.WorkoutExercise, ExerciseForListVm>()
                .ForMember(dest => dest.Sets, opt => opt.MapFrom(src => src.Sets))
                .ForMember(dest => dest.Reps, opt => opt.MapFrom(src => src.Reps))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Exercise.Name))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight));
        }
    }
}
