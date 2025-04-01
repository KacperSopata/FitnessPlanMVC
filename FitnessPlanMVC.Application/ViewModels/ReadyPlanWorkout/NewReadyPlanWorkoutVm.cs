using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.ReadyPlanWorkout
{
    public class NewReadyPlanWorkoutVm : IMapFrom<FitnessPlanMVC.Domain.Model.ReadyPlanWorkout>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlanType { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public string PlanDetails { get; set; }
        public string VideoUrl { get; set; }
        public string UserId { get; set; }
        public string ApplicationUser { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.ReadyPlanWorkout, NewReadyPlanWorkoutVm>();
            profile.CreateMap<NewReadyPlanWorkoutVm, FitnessPlanMVC.Domain.Model.ReadyPlanWorkout>();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.ReadyPlanWorkout, ReadyPlanWorkoutForListVm>();
        }
    }
}
