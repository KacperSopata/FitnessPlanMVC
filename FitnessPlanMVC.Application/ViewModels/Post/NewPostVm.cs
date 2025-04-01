using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitnessPlanMVC.Application.Mapping;
using Microsoft.AspNetCore.Http;

namespace FitnessPlanMVC.Application.ViewModels.Post
{
    public class NewPostVm : IMapFrom<FitnessPlanMVC.Domain.Model.Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string ApplicationUser { get; set; }
        public IFormFile ImageContent { get; set; }
        public byte[] Image { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FitnessPlanMVC.Domain.Model.Post, NewPostVm>().ReverseMap();
        }
    }

}
