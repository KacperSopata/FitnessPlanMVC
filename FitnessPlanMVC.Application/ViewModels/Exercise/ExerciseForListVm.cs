﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Exercise
{
    public class ExerciseForListVm : IMapFrom<FitnessPlanMVC.Domain.Model.Exercise>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Exercise, ExerciseForListVm>();
        }
    }
}
