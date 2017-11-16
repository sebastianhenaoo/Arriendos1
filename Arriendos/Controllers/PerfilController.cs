using Arriendos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arriendos.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataUsuarioActual()
        {
            string idusuario = User.Identity.GetUserId();
            BehaviorController behavior = new BehaviorController();
            return PartialView("_UsuarioLogeado", behavior.ActualUsuario(idusuario));
        }
        [HttpPost]
        public ActionResult InmuebleFoto(Propiedad propiedad, HttpPostedFileBase[] Fotos)
        {
            BehaviorController behavior = new BehaviorController();

            string idusuario = User.Identity.GetUserId();
            propiedad.IdUsuario = idusuario;
            if (behavior.CrearPropiedad(propiedad))
            {
                for (int i = 0; i < Fotos.Length; i++)
                {
                    if (Fotos[i] != null)
                    {
                        Foto foto = new Foto();
                        foto.Imagen = behavior.convertirImagen(Fotos[i]);
                        foto.IdPropiedad = propiedad.Id;
                        behavior.CrearFoto(foto);
                    }
                }
                
            }
            return View("Index");
        }

        public FileContentResult MostrarInmueblesUsuario()
        {
            string idusuario = User.Identity.GetUserId();
            var bd = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var fotos = bd.Fotos.Where(x => x.IdPropiedad == 15).FirstOrDefault();

            return new FileContentResult(fotos.Imagen, "image/jpeg");
        }
    }
}