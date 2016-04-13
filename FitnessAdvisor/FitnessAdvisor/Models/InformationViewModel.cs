using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessAdvisor.Models
{
    public class InformationViewModel
    {
        public UserInformation userinformation { get; set; }
        public Choice choice { get; set; }
        public WeightLoss weightloss {get; set;}
        public Strength strength { get; set; }


        private ActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}