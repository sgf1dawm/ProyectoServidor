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
    [Authorize(Roles = "Administrador")]
    public class TipoCochesController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();

        // GET: TipoCoches
        public ActionResult Index()
        {
            return View(db.TipoCoche.ToList());
        }

        // GET: TipoCoches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCoche tipoCoche = db.TipoCoche.Find(id);
            if (tipoCoche == null)
            {
                return HttpNotFound();
            }
            return View(tipoCoche);
        }

        // GET: TipoCoches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCoches/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoCoche,NombreTipo")] TipoCoche tipoCoche)
        {
            if (ModelState.IsValid)
            {
                db.TipoCoche.Add(tipoCoche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCoche);
        }

        // GET: TipoCoches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCoche tipoCoche = db.TipoCoche.Find(id);
            if (tipoCoche == null)
            {
                return HttpNotFound();
            }
            return View(tipoCoche);
        }

        // POST: TipoCoches/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoCoche,NombreTipo")] TipoCoche tipoCoche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCoche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoCoche);
        }

        // GET: TipoCoches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCoche tipoCoche = db.TipoCoche.Find(id);
            if (tipoCoche == null)
            {
                return HttpNotFound();
            }
            return View(tipoCoche);
        }

        // POST: TipoCoches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCoche tipoCoche = db.TipoCoche.Find(id);
            db.TipoCoche.Remove(tipoCoche);
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
