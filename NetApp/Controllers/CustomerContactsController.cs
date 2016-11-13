﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetApp.Models;

namespace NetApp.Controllers
{
    [Authorize]
    public class CustomerContactsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CustomerContacts
        public ActionResult Index()
        {
            var customerContacts = db.CustomerContacts.Include(c => c.Customer);
            return View(customerContacts.ToList());
        }

        // GET: CustomerContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            return View(customerContact);
        }

        // GET: CustomerContacts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: CustomerContacts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CustomerId")] CustomerContact customerContact)
        {
            if (ModelState.IsValid)
            {
                db.CustomerContacts.Add(customerContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customerContact.CustomerId);
            return View(customerContact);
        }

        // GET: CustomerContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customerContact.CustomerId);
            return View(customerContact);
        }

        // POST: CustomerContacts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CustomerId")] CustomerContact customerContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customerContact.CustomerId);
            return View(customerContact);
        }

        // GET: CustomerContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            return View(customerContact);
        }

        // POST: CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            db.CustomerContacts.Remove(customerContact);
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
