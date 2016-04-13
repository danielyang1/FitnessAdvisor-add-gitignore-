using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessAdvisor.Models;
using Microsoft.AspNet.Identity;

namespace FitnessAdvisor.Controllers
{
    public class WeightLossesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WeightLosses
        public ActionResult Index()
        {
            return View(db.WeightLosses.ToList());
        }
        public ActionResult StartHere()
        {
            return View();
        }
        // GET: WeightLosses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightLoss weightLoss = db.WeightLosses.Find(id);
            if (weightLoss == null)
            {
                return HttpNotFound();
            }
            return View(weightLoss);
        }

        // GET: WeightLosses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeightLosses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,caloriesBurnedSoFar,caloriesBurnedThisWorkout,expectedCaloriesBurned,caloriesEaten,amtOfWeightLoss,today,BMI,BMR,howActiveMultiplier,activityMultiplier")] WeightLoss weightLoss)
        {
            weightLoss.today = DateTime.Now.DayOfYear;
            if (ModelState.IsValid)
            {
                weightLoss.UserID = User.Identity.GetUserId();

                var alreadyEnteredInfo = db.WeightLosses.Where(u => u.UserID == weightLoss.UserID).FirstOrDefault();

                if (alreadyEnteredInfo != null)
                {
                    db.WeightLosses.RemoveRange(db.WeightLosses.Where(s => s.UserID == weightLoss.UserID));
                    db.SaveChanges();
                }

                weightLoss.UserID = User.Identity.GetUserId();
                var userid = User.Identity.GetUserId();//left off here last night danielyang
                var height = db.UserInformations.Where(u => u.UserID == userid).Select(h => h.height).FirstOrDefault();
                var weight = db.UserInformations.Where(u => u.UserID == userid).Select(w => w.weight).FirstOrDefault();
                var age = db.UserInformations.Where(u => u.UserID == userid).Select(a => a.age).FirstOrDefault();

                if (weightLoss.howActiveMultiplier == "VA")
                {
                    weightLoss.activityMultiplier = 1.725;
                }
                else if (weightLoss.howActiveMultiplier == "SA")
                {
                    weightLoss.activityMultiplier = 1.55;
                }
                else if (weightLoss.howActiveMultiplier == "NA")
                {
                    weightLoss.activityMultiplier = 1.2;
                }

                if (db.UserInformations.Where(u => u.UserID == userid).Select(s => s.sex).FirstOrDefault() == "Male")
                {
                    weightLoss.BMR = Math.Round((4.53592 * weight) + (15.875 * height) - (5 * age) + 5);
                    weightLoss.BMI = Math.Round((703 * weight / Math.Pow(height, 2)), 1);
                }
                else if (db.UserInformations.Where(u => u.UserID == userid).Select(s => s.sex).FirstOrDefault() == "Female")
                {
                    weightLoss.BMR = Math.Round((4.53592 * weight) + (15.875 * height) - (5 * age) - 161);
                    weightLoss.BMI = Math.Round((703 * weight / Math.Pow(height, 2)), 1);
                }


                db.WeightLosses.Add(weightLoss);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(weightLoss);
        }


        // GET: WeightLosses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightLoss weightLoss = db.WeightLosses.Find(id);
            if (weightLoss == null)
            {
                return HttpNotFound();
            }
            return View(weightLoss);
        }

        // POST: WeightLosses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,caloriesBurnedSoFar,caloriesBurnedThisWorkout,expectedCaloriesBurned,caloriesEaten,amtOfWeightLoss,today,BMI,BMR")] WeightLoss weightLoss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weightLoss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weightLoss);
        }

        // GET: WeightLosses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightLoss weightLoss = db.WeightLosses.Find(id);
            if (weightLoss == null)
            {
                return HttpNotFound();
            }
            return View(weightLoss);
        }

        // POST: WeightLosses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeightLoss weightLoss = db.WeightLosses.Find(id);
            db.WeightLosses.Remove(weightLoss);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
