using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arriendos.Models;
using Microsoft.AspNet.Identity;

namespace Arriendos.Controllers
{
    public class PropiedadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Propiedades
        [Authorize]
        public ActionResult Index()
        {

            var propiedades = db.propiedades.Where(p=> p.Estado==true).Include(p => p.ciudad).Include(p=>p.Fotos);
            return View(propiedades.ToList());
        }

        public ActionResult Galeria(int id)
        {
            BehaviorController behavior = new BehaviorController();
            var propiedad = db.propiedades.Where(p => p.Id == id).Include(p => p.ciudad).Include(p => p.Fotos).ToList();
            //var propiedad = behavior.PropiedadSeleccionada(id);
            return PartialView("_Galeria", propiedad);
        }
        // GET: Propiedades/Details/5
         public ActionResult Postular (int? id)
        {
             
            int idPropiedad = db.propiedades.Find(id).Id;
            string idusuario = User.Identity.GetUserId();
            BehaviorController behavior = new BehaviorController();
          
            Postular postular = new Postular()
            {
                IdPropiedad = idPropiedad,
                IdUsuario = idusuario

            };
            if (behavior.Postular(postular))
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        // GET: Propiedades/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdCiudad = new SelectList(db.ciudades, "Id", "Nombre");
            return PartialView("_Create");
        }

        // POST: Propiedades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Propiedad propiedad)
        {
            if (ModelState.IsValid)
            {
                // debo hacer un metodo que registre las fotos 
                string idusuario = User.Identity.GetUserId();
                propiedad.Estado = true;
                propiedad.IdUsuario = idusuario;
                db.propiedades.Add(propiedad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCiudad = new SelectList(db.ciudades, "Id", "Nombre", propiedad.IdCiudad);
            return View(propiedad);
        }

        // GET: Propiedades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propiedad propiedad = db.propiedades.Find(id);
            if (propiedad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCiudad = new SelectList(db.ciudades, "Id", "Nombre", propiedad.IdCiudad);
            return View(propiedad);
        }

        // POST: Propiedades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Direccion,Precio,IdCiudad,Descripcion,Estado")] Propiedad propiedad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCiudad = new SelectList(db.ciudades, "Id", "Nombre", propiedad.IdCiudad);
            return View(propiedad);
        }

        // GET: Propiedades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propiedad propiedad = db.propiedades.Find(id);
            if (propiedad == null)
            {
                return HttpNotFound();
            }
            return View(propiedad);
        }

        // POST: Propiedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propiedad propiedad = db.propiedades.Find(id);
            db.propiedades.Remove(propiedad);
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
