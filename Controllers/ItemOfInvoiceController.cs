using LaundryV3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LaundryV3.Controllers
{
    public class ItemOfInvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InvoiceItems
        public ActionResult Index(int? invId)
        {
            var invoiceItems = db.InvoiceItems.Where(i => i.InvoiceId == invId).Include(i => i.Invoice).Include(i => i.ServiceDetail);
            return View(invoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Description");
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name");
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Quantity,Code,ServiceDetailId,InvoiceId")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Description", invoiceItem.InvoiceId);
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name", invoiceItem.ServiceDetailId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices.Where(i => i.Id == invoiceItem.InvoiceId), "Id", "Description", invoiceItem.InvoiceId);
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name", invoiceItem.ServiceDetailId);
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Quantity,Code,ServiceDetailId,InvoiceId")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                InvoiceItem invoiceItemOld = db.InvoiceItems.Find(invoiceItem.Id);
                Invoice invoice = db.Invoices.Find(invoiceItemOld.InvoiceId);
                invoice.Price = invoice.Price - invoiceItemOld.Price;
                invoice.Price = invoice.Price + invoiceItem.Price;
                db.Entry(invoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + invoiceItem.InvoiceId, "Invoices");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Description", invoiceItem.InvoiceId);
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name", invoiceItem.ServiceDetailId);
            return RedirectToAction("Details/" + invoiceItem.InvoiceId, "Invoices");
        }

        // GET: InvoiceItems/AddItem/5
        public ActionResult AddItem(int? id)
        {
            ViewBag.InvoiceId = new SelectList(db.Invoices.Where(In => In.Id == id), "Id", "Description");
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name");
            InvoiceItem invoiceItem = new InvoiceItem();
            invoiceItem.InvoiceId = (int)id;
            return View(invoiceItem);
        }

        // POST: InvoiceItems/AddItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem([Bind(Include = "Id,Price,Quantity,Code,ServiceDetailId,InvoiceId")] InvoiceItem invoiceItem)
        {
            ServiceDetail serviceDetail = db.ServiceDetails.Find(invoiceItem.ServiceDetailId);
            invoiceItem.Price = invoiceItem.Quantity * serviceDetail.Price;
            invoiceItem.Code = invoiceItem.Id + serviceDetail.Id + (DateTimeOffset.Now.ToUnixTimeSeconds() - 1640000000).ToString();

            Invoice invoice = db.Invoices.Find(invoiceItem.InvoiceId);
            invoice.Price = invoice.Price + invoiceItem.Price;
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Details/" + invoiceItem.InvoiceId, "Invoices");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices.Where(In => In.Id == invoiceItem.InvoiceId));
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "Id", "Name", invoiceItem.ServiceDetailId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            db.InvoiceItems.Remove(invoiceItem);
            Invoice invoice = db.Invoices.Find(invoiceItem.InvoiceId);
            invoice.Price = invoice.Price - invoiceItem.Price;
            db.SaveChanges();
            return RedirectToAction("Details/" + invoiceItem.InvoiceId, "Invoices");
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