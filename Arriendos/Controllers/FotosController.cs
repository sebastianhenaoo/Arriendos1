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

        // GET: Fotos
        public ActionResult Index()
        {
            var fotos = db.Fotos.Include(f => f.propiedad);
            return View(fotos.ToList());
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
