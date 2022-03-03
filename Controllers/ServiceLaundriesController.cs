using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaundryV3.Models;

namespace LaundryV3.Controllers
{
    public class ServiceLaundriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceLaundries
        public ActionResult Index()
        {
            return View(db.ServiceLaundries.ToList());
        }

        // GET: ServiceLaundries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLaundry serviceLaundry = db.ServiceLaundries.Find(id);
            if (serviceLaundry == null)
            {
                return HttpNotFound();
            }
            return View(serviceLaundry);
        }

        // GET: ServiceLaundries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceLaundries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PriceFrom,PriceTo,Description")] ServiceLaundry serviceLaundry)
        {
            if (ModelState.IsValid)
            {
                db.ServiceLaundries.Add(serviceLaundry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceLaundry);
        }

        // GET: ServiceLaundries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLaundry serviceLaundry = db.ServiceLaundries.Find(id);
            if (serviceLaundry == null)
            {
                return HttpNotFound();
            }
            return View(serviceLaundry);
        }

        // POST: ServiceLaundries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PriceFrom,PriceTo,Description")] ServiceLaundry serviceLaundry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceLaundry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceLaundry);
        }

        // GET: ServiceLaundries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLaundry serviceLaundry = db.ServiceLaundries.Find(id);
            if (serviceLaundry == null)
            {
                return HttpNotFound();
            }
            return View(serviceLaundry);
        }

        // POST: ServiceLaundries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceLaundry serviceLaundry = db.ServiceLaundries.Find(id);
            db.ServiceLaundries.Remove(serviceLaundry);
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
