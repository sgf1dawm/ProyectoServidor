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
    public class TipoMotorsController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();

        // GET: TipoMotors
        public ActionResult Index()
        {
            return View(db.TipoMotor.ToList());
        }

        // GET: TipoMotors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMotor tipoMotor = db.TipoMotor.Find(id);
            if (tipoMotor == null)
            {
                return HttpNotFound();
            }
            return View(tipoMotor);
        }

        // GET: TipoMotors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMotors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMotor,NombreTipo")] TipoMotor tipoMotor)
        {
            if (ModelState.IsValid)
            {
                db.TipoMotor.Add(tipoMotor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMotor);
        }

        // GET: TipoMotors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMotor tipoMotor = db.TipoMotor.Find(id);
            if (tipoMotor == null)
            {
                return HttpNotFound();
            }
            return View(tipoMotor);
        }

        // POST: TipoMotors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMotor,NombreTipo")] TipoMotor tipoMotor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMotor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMotor);
        }

        // GET: TipoMotors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMotor tipoMotor = db.TipoMotor.Find(id);
            if (tipoMotor == null)
            {
                return HttpNotFound();
            }
            return View(tipoMotor);
        }

        // POST: TipoMotors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMotor tipoMotor = db.TipoMotor.Find(id);
            db.TipoMotor.Remove(tipoMotor);
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
