using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessAdvisor.Models
{
    public class CurrentWeight
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Please enter today's weight!")]
        public double weight { get; set; }
        public DateTime today { get; set; }

    }
}