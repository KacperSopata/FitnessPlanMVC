using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.StandardMeal;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.Services
{
    public class StandardMealService : IStandardMealService
    {
        private readonly IStandardMealRepository _mealRepo;
        private readonly IMapper _mapper;

        public StandardMealService(IStandardMealRepository standardMealRepo, IMapper mapper)
        {
            _mealRepo = standardMealRepo;
            _mapper = mapper;
        }
        public void AddMealsToDay(DateTime selectedData)
        {
            bool mealsExist = _mealRepo.MealsExistForDate(selectedData);
            if (!mealsExist)
            {
                List<StandardMealForListVm> mealsOfDay = new List<StandardMealForListVm>();
                string[] mealNames = { "Śniadanie", "II śniadanie", "Obiad", "Przekąska", "Kolacja" };
                foreach (var mealName in mealNames)
                {
                    StandardMealForListVm meal = new StandardMealForListVm
                    {
                        Name = mealName,
                        Data = selectedData,
                        Products = new List<StandardMealDetailVm>()
                    };
                    mealsOfDay.Add(meal);
                }
                var mappedMeals = _mapper.Map<List<Meal>>(mealsOfDay);
                _mealRepo.AddMeals(mappedMeals);
            }
        }
        public int AddProductMeal(NewProductStandardInMealVm productId)
        {
            // var prod = _mapper.Map<MealProduct>(productId);
            // var id = _mealRepo.AddProd(prod);
            // return id;
            return 0;
        }

        public void DeleteProduct(int id)
        {
            _mealRepo.DeleteProduct(id);
        }

        public ListStandardMealsForListVm GetAllMealsForList(DateTime selectedData)
        {
            var mealsFromDb = _mealRepo.GetAllMeals(selectedData)
                        .ProjectTo<FitnessPlanMVC.Application.ViewModels.StandardMeal.StandardMealForListVm>(_mapper.ConfigurationProvider)
                        .ToList();
            var combinedVm = new ListStandardMealsForListVm()
            {
                Meals = new List<StandardMealForListVm>(),
                Products = new List<StandardMealDetailVm>()
            };
            foreach (var meal in mealsFromDb)
            {
                combinedVm.Meals.Add(meal);
                if (meal.Products != null)
                {
                    foreach (var product in meal.Products)
                    {
                        var grammage = _mealRepo.GetGrammageForProduct(product.Id);
                        var productVm = _mapper.Map<StandardMealDetailVm>(product);
                        productVm.Grammage = grammage;
                        combinedVm.Products.Add(productVm);
                    }
                }
            }
            return combinedVm;
        }
        public ListStandardMealsForListVm GetMealById(int mealId)
        {
            var mealsFromDb = _mealRepo.GetAllMealsById(mealId)
                       .ProjectTo<FitnessPlanMVC.Application.ViewModels.StandardMeal.StandardMealForListVm>(_mapper.ConfigurationProvider)
                       .ToList();

            var combinedVm = new ListStandardMealsForListVm()
            {
                Meals = new List<StandardMealForListVm>(),
                Products = new List<StandardMealDetailVm>()
            };
            foreach (var meal in mealsFromDb)
            {
                combinedVm.Meals.Add(meal);
                if (meal.Products != null)
                {
                    foreach (var product in meal.Products)
                    {
                        var grammage = _mealRepo.GetGrammageForProduct(product.Id);

                        var productVm = _mapper.Map<StandardMealDetailVm>(product);
                        productVm.Grammage = grammage;
                        combinedVm.Products.Add(productVm);
                    }
                }
            }
            return combinedVm;
        }
    }
}