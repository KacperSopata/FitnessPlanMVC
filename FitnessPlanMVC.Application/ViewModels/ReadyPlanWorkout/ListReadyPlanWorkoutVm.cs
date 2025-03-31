using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.ReadyPlanWorkout
{
    public class ListReadyPlanWorkoutVm
    {
        public List<ReadyPlanWorkoutForListVm> ReadyPlanWorkouts { get; set; } // Lista planów treningowych
        public int CurrentPage { get; set; } // Aktualna strona dla paginacji
        public int PageSize { get; set; } // Ilość elementów na stronie
        public string SearchString { get; set; } // Wyszukiwane hasło
        public int Count { get; set; } // Łączna liczba planów
    }
}
