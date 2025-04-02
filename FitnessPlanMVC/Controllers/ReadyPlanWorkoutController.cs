using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.ReadyPlanWorkout;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Controllers
{
    public class ReadyPlanWorkoutController : Controller
    {
        private readonly IReadyPlanWorkoutService _readyPlanWorkoutService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReadyPlanWorkoutController(IReadyPlanWorkoutService readyPlanWorkoutService, UserManager<ApplicationUser> userManager)
        {
            _readyPlanWorkoutService = readyPlanWorkoutService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var model = _readyPlanWorkoutService.GetAllReadyPlanWorkoutsForList(10, 1, searchString ?? string.Empty);
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = userId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = string.Empty;
            }
            var model = _readyPlanWorkoutService.GetAllReadyPlanWorkoutsForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddPlanWorkout()
        {
            return View(new NewReadyPlanWorkoutVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPlanWorkout(NewReadyPlanWorkoutVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;

            if (ModelState.IsValid)
            {
                _readyPlanWorkoutService.AddReadyPlanWorkout(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetPlansByTypeAndDifficulty(string type, string difficulty)
        {
            // Debugging: sprawdź, czy parametry są poprawnie odbierane
            Console.WriteLine($"Received Type: {type}, Difficulty: {difficulty}");

            var results = _readyPlanWorkoutService.GetPlansByTypeAndDifficulty2(type, difficulty);
            return View(results);
        }



        [HttpGet]
        public IActionResult GetPlansByType(string type)
        {
            var plans = _readyPlanWorkoutService.GetPlansByType(type).ToList();
            return View(plans);
        }

        [HttpGet]
        public IActionResult GetPlansByDifficulty(string difficulty)
        {
            var plans = _readyPlanWorkoutService.GetPlansByDifficulty(difficulty).ToList();
            return View(plans);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new NewReadyPlanWorkoutVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(NewReadyPlanWorkoutVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;

            if (ModelState.IsValid)
            {
                _readyPlanWorkoutService.AddReadyPlanWorkout(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}