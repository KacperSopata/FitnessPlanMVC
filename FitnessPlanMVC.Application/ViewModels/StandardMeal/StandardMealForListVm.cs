using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.StandardMeal
{
    public class StandardMealForListVm : IMapFrom<FitnessPlanMVC.Domain.Model.Meal>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Data { get; set; }
        public List<StandardMealDetailVm> Products { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Meal, StandardMealForListVm>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.MealProducts.Select(mp => mp.Product)));
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Meal, StandardMealForListVm>();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.MealProduct, StandardMealForListVm>();
        }
    }
}
