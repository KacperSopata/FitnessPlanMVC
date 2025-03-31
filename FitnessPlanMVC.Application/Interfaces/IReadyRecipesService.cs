using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.ReadyRecipes;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IReadyRecipesService
    {
        ListReadyRecipesVm GetAllReadyRecipesForList(int pageSize, int pageNO, string searchString);
        int AddReadyRecipes(NewReadyRecipesVm model);
        ReadyRecipesDetailVm GetReadyRecipesDetail(int id);
        void DeleteReadyRecipes(int id);
    }
}
