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
    public class ChoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Choices
        public ActionResult Index()
        {
            return View(db.Choices.ToList());
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,typeOfWorkout")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                choice.UserID = User.Identity.GetUserId();//choice user is added

                var alreadyEnteredTypeOfWorkout = db.Choices.Where(u => u.UserID == choice.UserID).FirstOrDefault();

                if (alreadyEnteredTypeOfWorkout != null)
                {
                    db.Choices.RemoveRange(db.Choices.Where(s => s.UserID == choice.UserID));
                    db.SaveChanges();
                }

                db.Choices.Add(choice);
                db.SaveChanges();
                if (choice.typeOfWorkout == "Strength")
                {
                    return RedirectToAction("Index", "Strength");
                }
                else
                {
                    return RedirectToAction("Create", "WeightLosses");
                }
            }

            return View(choice);
        }

        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,typeOfWorkout")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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
