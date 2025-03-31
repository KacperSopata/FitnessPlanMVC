using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.StandardMeal
{
    public class StandardMealDetailVm : IMapFrom<FitnessPlanMVC.Domain.Model.Meal>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public float Grammage { get; set; }
        public List<StandardMealDetailVm> Products { get; set; }
        public List<StandardMealDetailVm> Meals { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Meal, StandardMealDetailVm>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.MealProducts));
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, StandardMealDetailVm>();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.MealProduct, StandardMealDetailVm>()
                     .ForMember(dest => dest.Grammage, opt => opt.MapFrom(src => src.Grammage));

        }
    }
}