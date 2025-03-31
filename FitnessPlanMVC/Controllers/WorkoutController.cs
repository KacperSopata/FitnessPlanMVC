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
        public WorkoutController(IWorkoutService workoutService,
            IExerciseService exerciseService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
        }

        public IActionResult Index(DateTime? selectedDate)
        {
            if (!selectedDate.HasValue || selectedDate == DateTime.MinValue)
            {
                selectedDate = DateTime.Today;
            }

            var model = _workoutService.GetWorkouts(selectedDate.Value); // <- nowa metoda
            ViewBag.SelectedDate = selectedDate.Value; // przydatne do formularza wyboru daty

            return View(model); // <- teraz model to List<WorkoutDetailVm>
        }


        [HttpGet]
        public IActionResult AddWorkoutToDay(DateTime workoutData)
        {
            var model = new NewWorkoutVm();
            model.StartWorkout = workoutData;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddWorkoutToDay(NewWorkoutVm model)
        {
            var id = _workoutService.AddWorkout(model);
            return RedirectToAction("Index", new { selectedDate = model.StartWorkout });
        }



        [HttpGet]
        public IActionResult AddExerciseToWorkout(int workoutId)
        {
            var exercises = _exerciseService.GetAllExercisesForList(10, 1, "");
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
        public IActionResult EditExercise2(int workoutId)
        {
            var exercise = _workoutService.GetWorkoutExerciseById(workoutId);
            return View(exercise);
        }

        [HttpPost]
        public IActionResult EditExercise2(NewWorkoutExerciseVm model)
        {
            _workoutService.UpdateExercise(model);
            return RedirectToAction("Index");
        }
    }
}