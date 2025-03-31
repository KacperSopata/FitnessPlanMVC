using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.StandardMeal
{
    public class NewProductStandardInMealVm
    {
        public int ProductId { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
    }
}
