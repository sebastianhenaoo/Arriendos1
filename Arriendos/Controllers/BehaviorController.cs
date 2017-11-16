using Arriendos.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arriendos.Controllers
{
    public class BehaviorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Behavior
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ApplicationUser ActualUsuario(string idusuario)
        {
            ApplicationUser Usuarioactual = db.Users.Find(idusuario);
            return Usuarioactual;
        }
        public bool CrearPropiedad(Propiedad propiedad)
        {

            if (ModelState.IsValid)
            {
                // debo hacer un metodo que registre las fotos 
                propiedad.Estado = true;
                db.propiedades.Add(propiedad);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool CrearFoto(Foto foto)
        {
            if (ModelState.IsValid)
            {
                db.Fotos.Add(foto);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public byte[] convertirImagen(HttpPostedFileBase foto)
        {
            byte[] imageData = null;           
            HttpPostedFileBase poImgFile = foto;

           using (var binary = new BinaryReader(poImgFile.InputStream))
           {
            imageData = binary.ReadBytes(poImgFile.ContentLength);
           }           
           return imageData;
        }

        public dynamic PropiedadesUsuarioActual(string idusuario)
        {
            var propiedades = db.propiedades.Where(p => p.IdUsuario == idusuario).ToList();
            return propiedades;
        }

        public bool Postular(Postular postulado)
        {
            if (ModelState.IsValid)
            {
                db.postulaciones.Add(postulado);
                db.SaveChanges();
                return true;
            }

            return false;


        }





    }

}