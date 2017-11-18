using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class PAGE_3Controller : ControllerExtends
    {
        // GET: PAGE_3
        public ActionResult PAGE_3()
        {
            if (Session["IdUser"] == null) return RedirectToAction("Login", "Login");
            int idUser = Convert.ToInt32(Session["IdUser"]);
            if (!ServiceManager.UserServices.AuthorizeRole(idUser, "PAGE_3") && !ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();
        }
    }
}