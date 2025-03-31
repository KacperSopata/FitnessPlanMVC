using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Workout
{
    public class NewWorkoutVm : IMapFrom<FitnessPlanMVC.Domain.Model.Workout>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartWorkout { get; set; }
        public List<ExerciseForListVm> Exercises { get; set; }
        public string? UserId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Workout, NewWorkoutVm>()
                .ReverseMap()
                .ForMember(dest => dest.StartWorkout, opt => opt.MapFrom(src => src.StartWorkout));
                
        }
    }
}