using System.Security.Claims;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Workout;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseService _exerciseService;
        private readonly UserManager<ApplicationUser> _userManager;
        public WorkoutController(UserManager<ApplicationUser> userManager, IWorkoutService workoutService,
            IExerciseService exerciseService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(DateTime? selectedDate)
        {
            if (!selectedDate.HasValue || selectedDate == DateTime.MinValue)
            {
                selectedDate = DateTime.Today;
            }

            var userId = _userManager.GetUserId(User);
            var workouts = _workoutService.GetWorkouts(userId, selectedDate.Value); // Zmieniamy na GetWorkouts, aby zwrócić listę
            foreach (var workout in workouts)
            {
                workout.StartWorkout = selectedDate.Value; // Przypisanie daty do wszystkich treningów
            }

            return View(workouts); // Zwrócenie listy workoutów
        }


        [HttpGet]
        public IActionResult AddWorkoutToDay(DateTime workoutData)
        {
            var model = new NewWorkoutVm();
            model.StartWorkout = workoutData;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutToDay(NewWorkoutVm model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            model.UserId = user.Id;
            var id = _workoutService.AddWorkout(model);

            DateTime workoutDate = model.StartWorkout;

            return RedirectToAction("Index", new { selectedDate = workoutDate });
        }

        [HttpGet]
        [HttpGet]
        public IActionResult AddExerciseToWorkout(int workoutId)
        {
            var exercises = _exerciseService.GetAllExercisesForList(100, 1, ""); // upewnij się, że jest odpowiednia ilość
            var model = new NewWorkoutExerciseVm()
            {
                Exercises = exercises.ExerciseForListVm,
                WorkoutId = workoutId
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddExerciseToWorkout(NewWorkoutExerciseVm model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            model.UserId = user.Id;

            var id = _workoutService.AddExerciseToWorkout(model);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWorkout(int workoutid, DateTime workoutData)
        {
            _workoutService.DeleteWorkout(workoutid);
            return RedirectToAction("Index", new { selectedDate = workoutData });
        }

        public IActionResult Delete(int id)
        {
            _workoutService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _exerciseService.GetExerciseDetailByWorkoutExercise(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditExercise2(int id)
        {
            var exercise = _workoutService.GetWorkoutExerciseById(id);

            if (exercise == null)
                return NotFound();

            return View(exercise);
        }


        [HttpPost]
        public IActionResult EditExercise2(NewWorkoutExerciseVm model)
        {
            var userId = _userManager.GetUserId(User);
            model.UserId = userId;
            _workoutService.UpdateExercise(model);
            return RedirectToAction("Index");
        }
    }
}