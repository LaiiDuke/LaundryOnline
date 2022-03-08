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
    public class ServiceDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceDetails
        public ActionResult Index()
        {
            var serviceDetails = db.ServiceDetails.Include(s => s.ServiceLaundry);
            return View(serviceDetails.ToList());
        }

        public ActionResult Public()
        {
            var serviceDetails = db.ServiceDetails.Include(s => s.ServiceLaundry);
            return View(serviceDetails.ToList());
        }



        // GET: ServiceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetails.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Create
        public ActionResult Create()
        {
            ViewBag.ServiceLaundryId = new SelectList(db.ServiceLaundries, "Id", "Name");
            return View();
        }

        // POST: ServiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Description,ServiceLaundryId")] ServiceDetail serviceDetail)
        {
            if (ModelState.IsValid)
            {
                db.ServiceDetails.Add(serviceDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceLaundryId = new SelectList(db.ServiceLaundries, "Id", "Name", serviceDetail.ServiceLaundryId);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetails.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceLaundryId = new SelectList(db.ServiceLaundries, "Id", "Name", serviceDetail.ServiceLaundryId);
            return View(serviceDetail);
        }

        // POST: ServiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Description,ServiceLaundryId")] ServiceDetail serviceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceLaundryId = new SelectList(db.ServiceLaundries, "Id", "Name", serviceDetail.ServiceLaundryId);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetails.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            return View(serviceDetail);
        }

        // POST: ServiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceDetail serviceDetail = db.ServiceDetails.Find(id);
            db.ServiceDetails.Remove(serviceDetail);
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
