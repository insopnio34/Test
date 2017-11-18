using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class PAGE_2Controller : ControllerExtends
    {
        // GET: PAGE_2
        public ActionResult PAGE_2()
        {
            if (Session["IdUser"] == null) return RedirectToAction("Login", "Login");
            int idUser = Convert.ToInt32(Session["IdUser"]);
            if (!ServiceManager.UserServices.AuthorizeRole(idUser, "PAGE_2") && !ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();
        }
    }
}