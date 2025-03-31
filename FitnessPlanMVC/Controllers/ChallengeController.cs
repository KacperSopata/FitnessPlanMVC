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
        private readonly UserManager<ApplicationUser> _userManager;

        public ChallengeController(IChallengeService challengeService, UserManager<ApplicationUser> userManager)
        {
            _challengeService = challengeService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = _challengeService.GetAllChallenges();
            return View(model);
        }

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
            var user = await _userManager.GetUserAsync(User);
            var model = _challengeService.GetUserChallenges(user.Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult MarkProgress(int challengeId, int progressToAdd = 1)
        {
            var userId = _userManager.GetUserId(User);
            _challengeService.UpdateUserProgress(challengeId, userId, progressToAdd);

            return RedirectToAction("MyChallenges");
        }

    }
}
