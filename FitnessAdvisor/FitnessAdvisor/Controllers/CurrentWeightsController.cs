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
    public class CurrentWeightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CurrentWeights
        public ActionResult Index()
        {
            return View(db.CurrentWeights.ToList());
        }
        public ActionResult DaysOver()
        {
            return View();
        }
        // GET: CurrentWeights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentWeight currentWeight = db.CurrentWeights.Find(id);
            if (currentWeight == null)
            {
                return HttpNotFound();
            }
            return View(currentWeight);
        }

        // GET: CurrentWeights/Create
        public ActionResult Create()
        {
            var thedate  = DateTime.Now;
            var thisdate = thedate.ToString("MMMM dd, yyyy");
            ViewBag.date = thisdate;
            return View();
        }

        // POST: CurrentWeights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,weight,today")] CurrentWeight currentWeight)
        {

            currentWeight.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var datetimenow = DateTime.Now;
                var userid = User.Identity.GetUserId();
                var todayCompleted = db.CurrentWeights.OrderByDescending( i => i.today).Where(u => u.UserID == userid).Select( t => t.today ).FirstOrDefault();
                if (todayCompleted.ToString("MM dd, yyyy") != datetimenow.ToString("MM dd, yyyy"))
                {
                    currentWeight.today = DateTime.Now;
                    db.CurrentWeights.Add(currentWeight);
                    db.SaveChanges();
                    return RedirectToAction("Exercises","Home");
                }
                //db.CurrentWeights.Add(currentWeight);
                //db.SaveChanges();
                return RedirectToAction("DaysOver","CurrentWeights");
            }

            return View(currentWeight);
        }

        // GET: CurrentWeights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentWeight currentWeight = db.CurrentWeights.Find(id);
            if (currentWeight == null)
            {
                return HttpNotFound();
            }
            return View(currentWeight);
        }

        // POST: CurrentWeights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,weight")] CurrentWeight currentWeight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentWeight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currentWeight);
        }

        // GET: CurrentWeights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentWeight currentWeight = db.CurrentWeights.Find(id);
            if (currentWeight == null)
            {
                return HttpNotFound();
            }
            return View(currentWeight);
        }

        // POST: CurrentWeights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentWeight currentWeight = db.CurrentWeights.Find(id);
            db.CurrentWeights.Remove(currentWeight);
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
