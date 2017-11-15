using Arriendos.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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


    }
}