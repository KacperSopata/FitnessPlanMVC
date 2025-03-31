using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.ReadyRecipes
{
    public class ReadyRecipesDetailVm : IMapFrom<FitnessPlanMVC.Domain.Model.ReadyRecipes>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public byte[] Image { get; set; } // Obecny obraz
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.ReadyRecipes, ReadyRecipesDetailVm>();
        }
    }
}
