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
    public class NewProductInUserInMealVm : IMapFrom<FitnessPlanMVC.Domain.Model.MealProduct>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MealId { get; set; }
        public float Grammage { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public string ApplicationUser { get; set; }
        public string UserId { get; set; }
        public List<Product.ProductForListVM> Products { get; set; }
        public void ConfigureMapping(Profile profile)
        {

            profile.CreateMap<MealProduct, NewProductInUserInMealVm>();
            profile.CreateMap<NewProductInUserInMealVm, MealProduct>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Grammage, opt => opt.MapFrom(src => src.Grammage))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, NewProductInUserInMealVm>()
                .ForMember(dest => dest.Calories, sou => sou.MapFrom(src => src.Calories))
                .ForMember(dest => dest.Protein, sou => sou.MapFrom(src => src.Protein))
                .ForMember(dest => dest.Fat, sou => sou.MapFrom(src => src.Fat))
                .ForMember(dest => dest.Carbohydrates, sou => sou.MapFrom(src => src.Carbohydrates));
        }
    }
}
