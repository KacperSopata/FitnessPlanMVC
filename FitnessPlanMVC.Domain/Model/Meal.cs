using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Domain.Model
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Data { get; set; }
        public ICollection<MealProduct> MealProducts { get; set; }
       
    }
}
