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
    public class PostularseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Postularse
        public ActionResult Index()
        {
            var postulaciones = db.postulaciones.Include(p => p.propiedad);
            return View(postulaciones.ToList());
        }

        // GET: Postularse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postular postular = db.postulaciones.Find(id);
            if (postular == null)
            {
                return HttpNotFound();
            }
            return View(postular);
        }

        // GET: Postularse/Create
        public ActionResult Create()
        {
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion");
            return View();
        }

        // POST: Postularse/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPropiedad")] Postular postular)
        {
            if (ModelState.IsValid)
            {
                db.postulaciones.Add(postular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", postular.IdPropiedad);
            return View(postular);
        }

        // GET: Postularse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postular postular = db.postulaciones.Find(id);
            if (postular == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", postular.IdPropiedad);
            return View(postular);
        }

        // POST: Postularse/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPropiedad")] Postular postular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPropiedad = new SelectList(db.propiedades, "Id", "Direccion", postular.IdPropiedad);
            return View(postular);
        }

        // GET: Postularse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postular postular = db.postulaciones.Find(id);
            if (postular == null)
            {
                return HttpNotFound();
            }
            return View(postular);
        }

        // POST: Postularse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Postular postular = db.postulaciones.Find(id);
            db.postulaciones.Remove(postular);
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
