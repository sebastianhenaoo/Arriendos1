using Microsoft.AspNet.Identity;
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
    }
}