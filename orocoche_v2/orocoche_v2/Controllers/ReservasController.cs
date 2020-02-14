using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using orocoche_v2.Models;

namespace orocoche_v2.Controllers
{

    public class ReservasController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Modelos).Include(r => r.Sedes).Include(r => r.Clientes);
            reservas = reservas.OrderByDescending(r => r.FechaInicio);
            return View(reservas.ToList());
        }

        //public ActionResult NuevaReserva(decimal id)
        //{

        //}


        // GET: Reservas/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre");
            ViewBag.Modelo = new SelectList(db.Modelos, "IdModelos", "Modelo");
            ViewBag.IdSede = new SelectList(db.Sedes, "IdSede", "Nombre");
            return View();
        }

        public ActionResult MisReservas()
        {
            // Se seleccionan los datos del empleado correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            var cliente = db.Clientes.Where(e => e.Email == wUsuario).FirstOrDefault();
            if (cliente == null)
            {
                // Si no existe el empleado, redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            // Si existe el empleado correspondiente se va a View
            return View(cliente);
        }
        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FechaInicio,FechaFin,IdCliente,Modelo,IdReserva,IdSede")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reservas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", reservas.IdCliente);
            ViewBag.Modelo = new SelectList(db.Modelos, "IdModelos", "Modelo", reservas.Modelo);
            ViewBag.IdSede = new SelectList(db.Sedes, "IdSede", "Nombre", reservas.IdSede);
            return View(reservas);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", reservas.IdCliente);
            ViewBag.Modelo = new SelectList(db.Modelos, "IdModelos", "Modelo", reservas.Modelo);
            ViewBag.IdSede = new SelectList(db.Sedes, "IdSede", "Nombre", reservas.IdSede);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FechaInicio,FechaFin,IdCliente,Modelo,IdReserva,IdSede")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre", reservas.IdCliente);
            ViewBag.Modelo = new SelectList(db.Modelos, "IdModelos", "Modelo", reservas.Modelo);
            ViewBag.IdSede = new SelectList(db.Sedes, "IdSede", "Nombre", reservas.IdSede);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Reservas reservas = db.Reservas.Find(id);
            db.Reservas.Remove(reservas);
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
