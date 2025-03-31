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
    public class UserNewMealVm : IMapFrom<FitnessPlanMVC.Domain.Model.Meal>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Data { get; set; }
        public List<ProductForListVm> Products { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Meal, UserNewMealVm>().ReverseMap();

        }
    }
}
