using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Product
{
    public class ProductDetailVm : IMapFrom<FitnessPlanMVC.Domain.Model.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, ProductDetailVm>();
        }
    }
}
