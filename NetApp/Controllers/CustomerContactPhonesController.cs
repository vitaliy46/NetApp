using System;
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
    public class CustomerContactPhonesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CustomerContactPhones
        public ActionResult Index()
        {
            var customerContactPhones = db.CustomerContactPhones.Include(c => c.CustomerContact);
            return View(customerContactPhones.ToList());
        }

        // GET: CustomerContactPhones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContactPhone customerContactPhone = db.CustomerContactPhones.Find(id);
            if (customerContactPhone == null)
            {
                return HttpNotFound();
            }
            return View(customerContactPhone);
        }

        // GET: CustomerContactPhones/Create
        public ActionResult Create()
        {
            ViewBag.CustomerContactId = new SelectList(db.CustomerContacts, "Id", "Name");
            return View();
        }

        // POST: CustomerContactPhones/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Phone,CustomerContactId")] CustomerContactPhone customerContactPhone)
        {
            if (ModelState.IsValid)
            {
                db.CustomerContactPhones.Add(customerContactPhone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerContactId = new SelectList(db.CustomerContacts, "Id", "Name", customerContactPhone.CustomerContactId);
            return View(customerContactPhone);
        }

        // GET: CustomerContactPhones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContactPhone customerContactPhone = db.CustomerContactPhones.Find(id);
            if (customerContactPhone == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerContactId = new SelectList(db.CustomerContacts, "Id", "Name", customerContactPhone.CustomerContactId);
            return View(customerContactPhone);
        }

        // POST: CustomerContactPhones/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Phone,CustomerContactId")] CustomerContactPhone customerContactPhone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerContactPhone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerContactId = new SelectList(db.CustomerContacts, "Id", "Name", customerContactPhone.CustomerContactId);
            return View(customerContactPhone);
        }

        // GET: CustomerContactPhones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContactPhone customerContactPhone = db.CustomerContactPhones.Find(id);
            if (customerContactPhone == null)
            {
                return HttpNotFound();
            }
            return View(customerContactPhone);
        }

        // POST: CustomerContactPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerContactPhone customerContactPhone = db.CustomerContactPhones.Find(id);
            db.CustomerContactPhones.Remove(customerContactPhone);
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
