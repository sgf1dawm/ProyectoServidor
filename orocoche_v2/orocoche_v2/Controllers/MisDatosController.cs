using Microsoft.AspNet.Identity;
using orocoche_v2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orocoche_v2.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class MisDatosController : Controller
    {
        private OroCocheEntities db = new OroCocheEntities();
        // GET: MisDatos
        public ActionResult Create()
        {
            return View();
        }
        // POST: MisDatos/Create

        // GET: MisDatos/Edit
        public ActionResult Edit()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
        "Id,Nombre,Email,DNI,FechaNacimiento")] Clientes cliente)
        {
            // Para asignar el Nombre del usuario identificado al campo Email de Empleado
            cliente.Email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                // Redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }
        // POST: MisDatos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Nombre,Email,DNI,FechaNacimiento")] Clientes cliente)
        {
            cliente.Email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }
    }
}