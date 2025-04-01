using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;
using Microsoft.AspNetCore.Http;

namespace FitnessPlanMVC.Application.ViewModels.ReadyRecipes
{
    public class NewReadyRecipesVm : IMapFrom<FitnessPlanMVC.Domain.Model.ReadyRecipes>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageContent { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string UserId { get; set; }
        public string ApplicationUser { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.ReadyRecipes, NewReadyRecipesVm>();
            profile.CreateMap<NewReadyRecipesVm, FitnessPlanMVC.Domain.Model.ReadyRecipes>();
            profile.CreateMap<FitnessPlanMVC.Domain.Model.ReadyRecipes, ReadyRecipesForListVm>();
        }
    }
}
