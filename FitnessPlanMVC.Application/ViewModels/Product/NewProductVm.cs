using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;
using FluentValidation;

namespace FitnessPlanMVC.Application.ViewModels.Product
{
    public class NewProductVm : IMapFrom<FitnessPlanMVC.Domain.Model.Product>
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        public int Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        //public string UserId { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Product, NewProductVm>().ReverseMap();
        }
        public class NewProductValidator : AbstractValidator<NewProductVm>
        {
            public NewProductValidator()
            {
                // RuleFor(x => x.Name).MaximumLength(5);
            }
        }
    }
}