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
    [Authorize(Roles = "Administrador,Usuario,UsuarioPremium")]
    public class ModelosController : Controller
    {

        private OroCocheEntities db = new OroCocheEntities();

        // GET: Modelos
        public ActionResult Index(string strTipo, string strMotor, string strMarca, string strCadenaBusqueda)
        {
            var modelos = db.Modelos.Include(m => m.Marca1).Include(m => m.TipoCoche).Include(m => m.TipoMotor);

            // Para presentar los tipos de avería en la vista
            var lstMarca = new List<string>();
            var qryMarca = from d in db.Marca
                           orderby d.NombreMarca
                           select d.NombreMarca;
            lstMarca.AddRange(qryMarca.Distinct());
            ViewBag.ListaMarca = new SelectList(lstMarca);

            var lstTipo = new List<string>();
            var qrytipo = from d in db.TipoCoche
                          orderby d.NombreTipo
                          select d.NombreTipo;
            lstTipo.AddRange(qrytipo.Distinct());
            ViewBag.ListaTipo = new SelectList(lstTipo);

            var lstMotor = new List<string>();
            var qryMotor = from d in db.TipoMotor
                           orderby d.NombreTipo
                           select d.NombreTipo;
            lstMotor.AddRange(qryMotor.Distinct());
            ViewBag.ListaMotor = new SelectList(lstMotor);

            // Para buscar avisos por nombre de empleado en la lista de valores
            if (!String.IsNullOrEmpty(strCadenaBusqueda))
            {
                modelos = modelos.Where(s => s.Modelo.Contains(strCadenaBusqueda));
            }

            // Para presentar los avisos filtrados por tipo de avería
            if (!string.IsNullOrEmpty(strMarca))
            {
                modelos = modelos.Where(x => x.Marca1.NombreMarca == strMarca);
            }

            if (!string.IsNullOrEmpty(strTipo))
            {
                modelos = modelos.Where(x => x.TipoCoche.NombreTipo == strTipo);
            }

            if (!string.IsNullOrEmpty(strMotor))
            {
                modelos = modelos.Where(x => x.TipoMotor.NombreTipo == strMotor);
            }
            if (User.IsInRole("Usuario"))
            {
                modelos = modelos.Where(x => x.Premium == false);
            }


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

            Session["ImagenAnterior"] = modelos.Imagen;

            ViewBag.ImagenAnterior = modelos.Imagen;
            ViewBag.Marca = new SelectList(db.Marca, "idMarca", "NombreMarca", modelos.Marca);
            ViewBag.Tipo = new SelectList(db.TipoCoche, "IdTipoCoche", "NombreTipo", modelos.Tipo);
            ViewBag.Motor = new SelectList(db.TipoMotor, "idMotor", "NombreTipo", modelos.Motor);
            return View(modelos);
        }

        public ActionResult NuevaReserva(decimal id, string strSede)
        {
            var lstSede = new List<string>();
            var qrySede = from d in db.Sedes
                          orderby d.Nombre
                          select d.Nombre;
            lstSede.AddRange(qrySede.Distinct());
            ViewBag.ListaSede = new SelectList(lstSede);

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
        public ActionResult Edit([Bind(Include = "IdModelos,Marca,Modelo,Potencia,Peso,Año,Tipo,Motor,Premium,Imagen")] Modelos modelos, HttpPostedFileBase Imagen)
        {
            if (ModelState.IsValid)
            {


                if (Imagen == null)
                {
                    modelos.Imagen = (string)Session["ImagenAnterior"];
                    Session["ImagenAnterior"] = null;
                }
                else
                {
                    modelos.Imagen = modelos.IdModelos + Path.GetExtension(Imagen.FileName);
                    Imagen.SaveAs(Server.MapPath("~/Content/Images/" +
                    modelos.Imagen));
                }
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
