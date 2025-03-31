using FitnessPlanMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public IActionResult Index(int pageNo = 1, int pageSize = 5, string searchString = "")
        {
            var model = _exerciseService.GetAllExercisesForList(pageSize, pageNo, searchString);
            model.PageSize = pageSize;
            model.CurrentPage = pageNo;
            model.SearchString = searchString;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }

            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _exerciseService.GetAllExercisesForList(pageSize, pageNo.Value, searchString);
            return View(model);

        }
        public IActionResult Details(int id)
        {
            var model = _exerciseService.GetExerciseDetail(id);
            return View(model);
        }
    }
}