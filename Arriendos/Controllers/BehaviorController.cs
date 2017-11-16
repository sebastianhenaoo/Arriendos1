using Arriendos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var propiedades = db.propiedades.Where(p => p.IdUsuario == idusuario).Include(p => p.Fotos).ToList();
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


        [Authorize]
        public FileContentResult Photos(int id)
        {
            if (id <= 0)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");

            }
            // to get the user details to load user Image 
            var bd = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var Image = bd.Fotos.Where(x => x.Id == id).FirstOrDefault();

            return new FileContentResult(Image.Imagen, "image/jpeg");
        }
    }

}