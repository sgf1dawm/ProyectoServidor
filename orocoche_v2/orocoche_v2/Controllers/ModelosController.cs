using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using orocoche_v2.Models;

namespace orocoche_v2.Controllers
{

    public class ModelosController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();

        // GET: Modelos
        public ActionResult Index()
        {
            var modelos = db.Modelos.Include(m => m.Marca1).Include(m => m.TipoCoche).Include(m => m.TipoMotor);
            return View(modelos.ToList());
        }

        // GET: Modelos/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelos modelos = db.Modelos.Find(id);
            if (modelos == null)
            {
                return HttpNotFound();
            }
            return View(modelos);
        }

        // GET: Modelos/Create
        public ActionResult Create()
        {
            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca");
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo");
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo");
            return View();
        }

        // POST: Modelos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdModelos,Marca,Modelo,Potencia,Peso,Año,Tipo,Motor,Premium, Imagen")] Modelos modelos, HttpPostedFileBase Imagen)
        {
            if (ModelState.IsValid)
            {

                db.Modelos.Add(modelos);
                db.SaveChanges();
                modelos.Imagen = modelos.IdModelos + Path.GetExtension(Imagen.FileName);
                db.Entry(modelos).State = EntityState.Modified;
                db.SaveChanges();
                Imagen.SaveAs(Server.MapPath("~/Content/Images/" +
                modelos.Imagen));



                return RedirectToAction("Index");
            }

            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca", modelos.Marca);
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo", modelos.Tipo);
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo", modelos.Motor);
            return View(modelos);
        }

        // GET: Modelos/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelos modelos = db.Modelos.Find(id);
            if (modelos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca", modelos.Marca);
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo", modelos.Tipo);
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo", modelos.Motor);
            return View(modelos);
        }

        public ActionResult NuevaReserva(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelos modelos = db.Modelos.Find(id);
            if (modelos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca", modelos.Marca);
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo", modelos.Tipo);
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo", modelos.Motor);
            return View(modelos);
        }

        // POST: Modelos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdModelos,Marca,Modelo,Potencia,Peso,Año,Tipo,Motor,Premium")] Modelos modelos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca", modelos.Marca);
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo", modelos.Tipo);
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo", modelos.Motor);
            return View(modelos);
        }

        // GET: Modelos/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelos modelos = db.Modelos.Find(id);
            if (modelos == null)
            {
                return HttpNotFound();
            }
            return View(modelos);
        }

        // POST: Modelos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Modelos modelos = db.Modelos.Find(id);
            DirectoryInfo Directorio = new
            DirectoryInfo(Server.MapPath("~/Content/Images/"));
            FileInfo[] Imagenes = Directorio.GetFiles(modelos.Imagen);
            if (Imagenes.Count() > 0)
            {
                Imagenes[0].Delete();
            }
            db.Modelos.Remove(modelos);
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
