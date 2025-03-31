using FitnessPlanMVC.Application.Mapping;
using FitnessPlanMVC.Domain.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.UserMeal
{
    public class UserMealDetailVm : IMapFrom<FitnessPlanMVC.Domain.Model.Meal>
    {
        public DateTime Data { get; set; }
        public List<UserMealForListVm> Meals { get; set; }
  
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Meal, UserMealDetailVm>().ReverseMap();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, ProductForListVm>();
            profile.CreateMap<MealProduct, UserMealDetailVm>().ReverseMap();
        }
    }
}
                                        