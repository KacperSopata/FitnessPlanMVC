using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Exercise
{
    public class NewExerciseVm : IMapFrom<FitnessPlanMVC.Domain.Model.Exercise>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
        public string Tips { get; set; }
        public string InvolvedParties { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Exercise, NewExerciseVm>().ReverseMap();
        }
    }
}