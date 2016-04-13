using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessAdvisor.Models
{
    public class UserInformation
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required]
        [Display(Name = "Age")]
        public double age { get; set; }
        [Required]
        [Display(Name = "Weight")]
        public double weight { get; set; }
        [Required]
        [Display(Name = "Height (in inches)")]
        public double height { get; set; }
        [Required]
        [Display(Name = "Male / Female")]
        public string sex { get; set; }
    }
}