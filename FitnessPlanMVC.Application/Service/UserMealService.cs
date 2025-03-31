using AutoMapper;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Product;
using FitnessPlanMVC.Application.ViewModels.UserMeal;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.Services
{
    public class UserMealService : IUserMealService
    {
        private readonly IUserMealRepository _UserMealRepo;
        private readonly IMapper _mapper;

        public UserMealService(IUserMealRepository UserMealRepo, IMapper mapper)
        {
            _UserMealRepo = UserMealRepo;
            _mapper = mapper;
        }

        public int AddMeal(UserNewMealVm model)
        {
            var meal = _mapper.Map<Meal>(model);
            var id = _UserMealRepo.AddMeal(meal);
            return id;
        }

        public int AddProductToMeal(NewProductInUserInMealVm model)
        {
            var prod = _mapper.Map<MealProduct>(model);
            var id = _UserMealRepo.AddProduct(prod);
            return id;
        }

        public void DeleteMeal(int mealid)
        {
            _UserMealRepo.DeleteMeal(mealid);
        }

        public void DeleteProduct(int id)
        {
            _UserMealRepo.DeleteProduct(id);
        }

        public bool DoesProductExistInMeal(int modelMealId, int modelProductId)
        {
            return _UserMealRepo.DoesProductExistInMeal(modelMealId, modelProductId);
        }

        public UserMealDetailVm GetMeal(string userId, DateTime selectedDate)
        {
            var meals = _UserMealRepo.GetMeal(selectedDate, userId);

            var mealVm = new UserMealDetailVm();
            mealVm.Meals = new List<UserMealForListVm>();
            mealVm.Data = selectedDate;

            foreach (var meal in meals)
            {
                var mealForListVm = _mapper.Map<UserMealForListVm>(meal);
                mealVm.Meals.Add(mealForListVm);

                var products = _UserMealRepo.GetProduct(meal.Id, userId);

                if (products != null && products.Any())
                {
                    mealForListVm.Products = new List<ProductForListVm>();

                    foreach (var product in products)
                    {
                        var productVm = _mapper.Map<ProductForListVm>(product);
                        productVm.Grammage = _UserMealRepo.GetGrammageForProduct(product.Id, meal.Id);
                        mealForListVm.Products.Add(productVm);
                    }
                }
            }

            return mealVm;
        }
        public DateTime GetMealDate(int modelMealId)
        {
            var mealData = _UserMealRepo.GetMealData(modelMealId);
            return mealData;
        }
        public NewProductInUserInMealVm GetMealProductById(int mealId)
        {
            var product = _UserMealRepo.GetMealProductById(mealId);
            var productVm = _mapper.Map<NewProductInUserInMealVm>(product);
            return productVm;
        }

        public void UpdateProduct(NewProductInUserInMealVm model)
        {
            var mealproduct = _mapper.Map<MealProduct>(model);
            _UserMealRepo.UpdateProduct(mealproduct);
        }
    }
}
