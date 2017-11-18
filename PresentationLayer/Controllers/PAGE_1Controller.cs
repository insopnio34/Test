using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class PAGE_1Controller : ControllerExtends
    {
        // GET: PAGE_1
        public ActionResult PAGE_1()
        {

            if (Session["IdUser"] == null) return RedirectToAction("Login", "Login");
            int idUser = Convert.ToInt32(Session["IdUser"]);
            if (!ServiceManager.UserServices.AuthorizeRole(idUser, "PAGE_1") && !ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();

        }

        public ActionResult PruebaJson()
        {
            return View();
        }
    }
}