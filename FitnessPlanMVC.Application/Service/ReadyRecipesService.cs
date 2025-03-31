using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using FitnessPlanMVC.Application.ViewModels.ReadyRecipes;

namespace FitnessPlanMVC.Application.Services
{
    public class ReadyRecipesService : IReadyRecipesService

    {
        private readonly IReadyRecipesRepository _readyRecipesRepo;
        private readonly IMapper _mapper;

        public ReadyRecipesService(IReadyRecipesRepository readyRecipesRepository, IMapper mapper)
        {
            _readyRecipesRepo = readyRecipesRepository;
            _mapper = mapper;
        }
        public int AddReadyRecipes(NewReadyRecipesVm model)
        {
            var readyRecipes = _mapper.Map<ReadyRecipes>(model);
            if (model.ImageContent != null)
            {
                readyRecipes.Image = model.Image;
            }
            var id = _readyRecipesRepo.AddReadyRecipes(readyRecipes);
            return id;
        }

        public void DeleteReadyRecipes(int id)
        {
            var recipe = _readyRecipesRepo.GetDetail(id);

            if (recipe != null)
            {
                _readyRecipesRepo.DeleteReadyRecipes(recipe); // Wywołaj metodę usuwającą w repozytorium
            }
        }

        public ListReadyRecipesVm GetAllReadyRecipesForList(int pageSize, int pageNo, string searchString)
        {
            var readyRecipesQuery = _readyRecipesRepo.GetAllReadyRecipes();


            if (!string.IsNullOrEmpty(searchString))
            {
                readyRecipesQuery = readyRecipesQuery
                    .AsEnumerable()
                    .Where(r => r.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .AsQueryable();
            }

            var recipes = readyRecipesQuery
                .ProjectTo<ReadyRecipesForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var readyRecipesList = new ListReadyRecipesVm
            {
                ReadyRecipes = recipes.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                PageSize = pageSize,
                Count = recipes.Count,
                SearchString = searchString
            };

            return readyRecipesList;
        }

        public ReadyRecipesDetailVm GetReadyRecipesDetail(int id)
        {
            var readyRecipesDetails = _readyRecipesRepo.GetDetail(id);
            var readyRecipesDetailsVm = _mapper.Map<ReadyRecipesDetailVm>(readyRecipesDetails);
            return readyRecipesDetailsVm;
        }
    }
}