using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;

namespace FitnessPlanMVC.Application.ViewModels.Product
{
    public class ProductForListVM : IMapFrom<FitnessPlanMVC.Domain.Model.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, FitnessPlanMVC.Application.ViewModels.Product.ProductForListVM>(); //w nawiasach pierwsze to zrodlo drugie to destynacja. J
        }

    }
}
