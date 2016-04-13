using FitnessAdvisor.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessAdvisor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            InformationViewModel info = new InformationViewModel();

            
            double[] weightArray = { 151.5, 150, 149, 148.5, 148, 147.5, 147,146.5, 146, 145.8 }; //gray and EXPECTED
            ViewBag.intArray = weightArray;

            double[] expectedWeightArray = { 151.3, 151, 149.9, 148.5, 147, 146.8, 146, 146.8, 146, 145.1 }; // need expected weight loss controller
            ViewBag.expArray = expectedWeightArray;

            var userid = User.Identity.GetUserId();
            ViewBag.firstname = db.UserInformations.Where(x => x.UserID == userid).Select(y => y.firstName).FirstOrDefault();
            ViewBag.weight = db.UserInformations.Where(x => x.UserID == userid).Select(y => y.weight).FirstOrDefault();
            ViewBag.height = db.UserInformations.Where(x => x.UserID == userid).Select(h => h.height).FirstOrDefault();
            ViewBag.age = db.UserInformations.Where(x => x.UserID == userid).Select(y => y.age).FirstOrDefault();

            ViewBag.bmi = db.WeightLosses.Where(x => x.UserID == userid).Select(y => y.BMI).FirstOrDefault();
            ViewBag.bmr = db.WeightLosses.Where(x => x.UserID == userid).Select(y => y.BMR).FirstOrDefault();

            var bmr = db.WeightLosses.Where(x => x.UserID == userid).Select(y => y.BMR).FirstOrDefault();
            var mult = db.WeightLosses.Where(x => x.UserID == userid).Select(y => y.activityMultiplier).FirstOrDefault();
            var calNeededPerDay = bmr * mult;
            ViewBag.recommendedCalIntake = Math.Round(calNeededPerDay);
            ViewBag.calBurned = bmr + 346;
            //List<string> hi = new List<string>(){ "a", "b", "c" }; testing graph labels only
            //ViewBag.hello = hi;

            db.SaveChanges();
            return View();
        }
        public ActionResult Exercises()
        {
            return View();
        }
        public ActionResult Meal()
        {
            ViewBag.date = DateTime.Now.ToString("MMMM dd, yyyy");
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}