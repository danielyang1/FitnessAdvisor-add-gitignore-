using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessAdvisor.Models;

namespace FitnessAdvisor.Controllers
{
    public class ExpectedWeightLossesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpectedWeightLosses
        public ActionResult Index()
        {
            return View(db.ExpectedWeightLosses.ToList());
        }

        // GET: ExpectedWeightLosses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedWeightLoss expectedWeightLoss = db.ExpectedWeightLosses.Find(id);
            if (expectedWeightLoss == null)
            {
                return HttpNotFound();
            }
            return View(expectedWeightLoss);
        }

        // GET: ExpectedWeightLosses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpectedWeightLosses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Weight1,Weight2,Weight3,Weight4,Weight5,Weight6,Weight7,Weight8,Weight9,Weight10,Weight11,Weight12,Weight13,Weight14,Weight15")] ExpectedWeightLoss expectedWeightLoss)
        {
            if (ModelState.IsValid)
            {
                db.ExpectedWeightLosses.Add(expectedWeightLoss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expectedWeightLoss);
        }

        // GET: ExpectedWeightLosses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedWeightLoss expectedWeightLoss = db.ExpectedWeightLosses.Find(id);
            if (expectedWeightLoss == null)
            {
                return HttpNotFound();
            }
            return View(expectedWeightLoss);
        }

        // POST: ExpectedWeightLosses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Weight1,Weight2,Weight3,Weight4,Weight5,Weight6,Weight7,Weight8,Weight9,Weight10,Weight11,Weight12,Weight13,Weight14,Weight15")] ExpectedWeightLoss expectedWeightLoss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expectedWeightLoss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expectedWeightLoss);
        }

        // GET: ExpectedWeightLosses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpectedWeightLoss expectedWeightLoss = db.ExpectedWeightLosses.Find(id);
            if (expectedWeightLoss == null)
            {
                return HttpNotFound();
            }
            return View(expectedWeightLoss);
        }

        // POST: ExpectedWeightLosses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpectedWeightLoss expectedWeightLoss = db.ExpectedWeightLosses.Find(id);
            db.ExpectedWeightLosses.Remove(expectedWeightLoss);
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
