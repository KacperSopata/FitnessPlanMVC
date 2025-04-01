using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class MealProduct
    {
        public int Id { get; set; }
        public int MealsId { get; set; }
        public Meal Meal { get; set; }
        public int ProductsId { get; set; }
        public Product Product { get; set; }
        public int Grammage { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
