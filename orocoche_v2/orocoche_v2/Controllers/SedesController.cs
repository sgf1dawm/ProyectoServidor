using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using orocoche_v2.Models;

namespace orocoche_v2.Controllers
{

    public class SedesController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();

        // GET: Sedes
        public ActionResult Index()
        {
            return View(db.Sedes.ToList());
        }

        // GET: Sedes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sedes sedes = db.Sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // GET: Sedes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sedes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSede,Nombre,Direccion")] Sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.Sedes.Add(sedes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sedes);
        }

        // GET: Sedes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sedes sedes = db.Sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // POST: Sedes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSede,Nombre,Direccion")] Sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sedes);
        }

        // GET: Sedes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sedes sedes = db.Sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // POST: Sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Sedes sedes = db.Sedes.Find(id);
            db.Sedes.Remove(sedes);
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
