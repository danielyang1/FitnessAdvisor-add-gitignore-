using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessAdvisor.Models
{
    public class Choice
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Your Workout")]
        public string typeOfWorkout { get; set; }
    }
}