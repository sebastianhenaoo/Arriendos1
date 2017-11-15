using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arriendos.Models;

namespace Arriendos.Controllers
{
    public class FotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fotoes
        public ActionResult Index()
        {
            var fotos = db.Fotos.Include(f => f.propiedad);
            return View(fotos.ToList());
        }

        // GET: Fotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Fotos.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            return View(foto);
        }

        // GET: Fotoes/Create
        public ActionResult Create()
        {
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion");
            return View();
        }

        // POST: Fotoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imagen,IdPropiedad")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                db.Fotos.Add(foto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", foto.IdPropiedad);
            return View(foto);
        }

        // GET: Fotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Fotos.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", foto.IdPropiedad);
            return View(foto);
        }

        // POST: Fotoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imagen,IdPropiedad")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", foto.IdPropiedad);
            return View(foto);
        }

        // GET: Fotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Fotos.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            return View(foto);
        }

        // POST: Fotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foto foto = db.Fotos.Find(id);
            db.Fotos.Remove(foto);
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
