using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Challenge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Controllers
{
    [Authorize]
    public class ChallengeController : Controller
    {
        private readonly IChallengeService _challengeService;
        //private readonly IChallengeProgressService _challengeProgressService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChallengeController(IChallengeService challengeService, UserManager<ApplicationUser> userManager /*IChallengeProgressService challengeProgressService*/)
        {
            _challengeService = challengeService;
            _userManager = userManager;
            //_challengeProgressService = challengeProgressService;
        }

        public IActionResult Index()
        {
            var model = _challengeService.GetAllChallenges();
            return View(model);
        }

        //[HttpPost]
        //public IActionResult AddProgress(int challengeId)
        //{
        //    var userId = _userManager.GetUserId(User); // Pobranie ID użytkownika

        //    try
        //    {
        //        // Debugowanie: Sprawdzamy, czy postęp jest dodawany
        //        Console.WriteLine($"UserId: {userId}, ChallengeId: {challengeId}");

        //        _challengeProgressService.AddProgress(challengeId, userId); // Wywołanie metody serwisu
        //        TempData["SuccessMessage"] = "Progress updated successfully!";
        //    }
        //    catch (Exception ex)
        //    {
        //        // Zgłaszanie błędu, jeśli coś poszło nie tak
        //        TempData["ErrorMessage"] = ex.Message;
        //    }

        //    // Po dodaniu postępu przekierowanie do widoku "MyChallenges"
        //    return RedirectToAction("MyChallenges");
        //}


        [HttpGet]
        public IActionResult Add()
        {
            return View(new NewChallengeVm());
        }

        [HttpPost]
        public IActionResult Add(NewChallengeVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _challengeService.AddChallenge(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _challengeService.GetChallengeById(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var challenge = _challengeService.GetChallengeById(id);
            var model = new NewChallengeVm
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Description = challenge.Description,
                DurationInDays = challenge.DurationInDays,
                StartDate = challenge.StartDate
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(NewChallengeVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _challengeService.UpdateChallenge(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _challengeService.DeleteChallenge(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Join(int challengeId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && !_challengeService.IsUserEnrolled(user.Id, challengeId))
            {
                _challengeService.AssignUserToChallenge(challengeId, user.Id);
            }

            return RedirectToAction("MyChallenges");
        }

        public async Task<IActionResult> MyChallenges()
        {
            var userId = _userManager.GetUserId(User);
            var challenges = _challengeService.GetUserChallenges(userId); // Pobierz dane o wyzwaniach dla danego użytkownika
            return View(challenges);
        }


        [HttpPost]
        public IActionResult MarkProgress(int challengeId, int progressToAdd = 1)
        {
            var userId = _userManager.GetUserId(User);

            // Sprawdzamy, czy progres jest aktualizowany poprawnie
            _challengeService.UpdateUserProgress(challengeId, userId, progressToAdd);

            // Po zaktualizowaniu progresu, pobierzmy najnowsze dane o wyzwaniach i przekazujmy je do widoku
            var model = _challengeService.GetUserChallenges(userId);
            return View("MyChallenges", model);  // Przekazujemy model zaktualizowany do widoku
        }

    }
}
