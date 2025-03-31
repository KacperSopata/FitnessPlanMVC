using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Challenge
{
    public class ChallengeForListVm : IMapFrom<FitnessPlanMVC.Domain.Model.Challenge>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Challenge, ChallengeForListVm>();
        }
    }
}