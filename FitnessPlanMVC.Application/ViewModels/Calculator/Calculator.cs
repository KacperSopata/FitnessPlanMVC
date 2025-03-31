using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPlanMVC.Application.ViewModels.Calculator
{
    public class Calculator
    {
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public List<string> Genders { get; set; } = new List<string> { "Man", "Woman" };
        [Display(Name = "Objective")]
        public string Goal { get; set; }
        public List<string> Goals { get; set; } = new List<string> { "Reduction", "Maintenance", "Mass" };
        [Display(Name = "PAL activity factor")]
        public double Pal { get; set; }
        public Dictionary<double, string> Pals { get; set; } = new Dictionary<double, string>
    {
        { 1.2, "Sedentary lifestyle" },
        { 1.4, "Low activity" },
        { 1.6, "Moderate activity" },
        { 1.8, "High activity" },
        { 2.0, "Very high activity" }
    };
        public double Weight { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double? CPM { get; set; }
        public double? PPM { get; set; }

    }
}