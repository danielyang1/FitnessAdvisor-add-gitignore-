using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessAdvisor.Models
{
    public class WeightLoss
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        private double caloriesToLoseOnePound = 3500;
        [Display(Name = "Calories burned so far")]
        public double? caloriesBurnedSoFar { get; set; }
        [Display(Name = "Calories burned this workout")]
        public double? caloriesBurnedThisWorkout { get; set; }
        public double? expectedCaloriesBurned { get; set; }
        public double? caloriesEaten { get; set; }
        public double? amtOfWeightLoss { get; set; }
        public int? today { get; set; }
        public double BMI { get; set; }
        public double BMR { get; set; }
        public double activityMultiplier { get; set; }
        public string howActiveMultiplier { get; set; }
    }
}