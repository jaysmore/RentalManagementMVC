using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class InvoicesController : Controller
    {
        private RentalManagementEntities db = new RentalManagementEntities();

        // GET: Invoices
        public ActionResult Index(string searchString)
        {
            var invoices = from m in db.Invoices select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = db.Invoices.Include(i => i.Job).Include(i => i.Rental).Include(i => i.Vendor).Include(i => i.Equipment).Where(s => s.invoiceID.ToString().Contains(searchString));
            }
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.jobID = new SelectList(db.Jobs, "jobID", "jobID");
            ViewBag.rentalID = new SelectList(db.Rentals, "rentalID", "rentalID");
            ViewBag.vendorID = new SelectList(db.Vendors, "vendorID", "vendorID");
            ViewBag.equipmentID = new SelectList(db.Equipments, "equipmentID", "equipmentID");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceID,invoiceNum,jobID,rentalID,vendorID,equipmentID,amount")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jobID = new SelectList(db.Jobs, "jobID", "jobID", invoice.jobID);
            ViewBag.rentalID = new SelectList(db.Rentals, "rentalID", "rentalID", invoice.rentalID);

            ViewBag.vendorID = new SelectList(db.Vendors, "vendorID", "vendorID", invoice.vendorID);
            ViewBag.equipmentID = new SelectList(db.Equipments, "equipmentID", "equipmentID", invoice.equipmentID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.jobID = new SelectList(db.Jobs, "jobID", "jobID", invoice.jobID);
            ViewBag.rentalID = new SelectList(db.Rentals, "rentalID", "rentalID", invoice.rentalID);
            ViewBag.vendorID = new SelectList(db.Vendors, "vendorID", "vendorID", invoice.vendorID);
            ViewBag.equipmentID = new SelectList(db.Equipments, "equipmentID", "equipmentID", invoice.equipmentID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoiceID,invoiceNum,jobID,rentalID,vendorID,equipmentID,amount")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jobID = new SelectList(db.Jobs, "jobID", "jobID", invoice.jobID);
            ViewBag.rentalID = new SelectList(db.Rentals, "rentalID", "rentalID", invoice.rentalID);
            ViewBag.vendorID = new SelectList(db.Vendors, "vendorID", "vendorID", invoice.vendorID);
            ViewBag.equipmentID = new SelectList(db.Equipments, "equipmentID", "equipmentID", invoice.equipmentID);
            return View(invoice);
        }

        public ActionResult Game()
        {
            return View();
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
