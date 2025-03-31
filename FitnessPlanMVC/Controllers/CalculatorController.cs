using FitnessPlanMVC.Application.ViewModels.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPlanMVC.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Calculator();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Calculator model)
        {
            if (ModelState.IsValid)
            {
                double ppm;
                if (model.Gender == "Kobieta")
                {
                    ppm = 655.1 + (9.563 * model.Weight) + (1.85 * model.Height) - (4.676 * model.Age);
                }
                else
                {
                    ppm = 66.5 + (13.75 * model.Weight) + (5.003 * model.Height) - (6.775 * model.Age);
                }
                //ppm *= model.Pal;
                switch (model.Goal)
                {
                    case "Redukcja":
                        ppm -= 200;
                        break;
                    case "Utrzymanie":

                        break;
                    case "Masa":
                        ppm += 200;
                        break;
                }
                model.PPM = ppm;
                model.CPM = ppm * model.Pal;
                return View(model);
            }

            return View(model);
        }
    }
}