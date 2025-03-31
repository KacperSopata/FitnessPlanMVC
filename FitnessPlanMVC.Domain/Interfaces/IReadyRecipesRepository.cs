using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IReadyRecipesRepository
    {
        IQueryable<ReadyRecipes> GetAllReadyRecipes();
        int AddReadyRecipes(ReadyRecipes readyRecipes);
        ReadyRecipes GetDetail(int id);
        void DeleteReadyRecipes(ReadyRecipes readyRecipes);
    }
}
