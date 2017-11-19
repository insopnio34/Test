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

            if (Session["UserName"] == null) return RedirectToAction("Login", "Login");
            if (!ServiceManager.UserServices.AuthorizeRole(Session["UserName"].ToString(), "PAGE_1") && !ServiceManager.UserServices.AuthorizeRole(Session["UserName"].ToString(), "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();

        }

        public ActionResult PruebaJson()
        { if (Session["UserName"] == null) return RedirectToAction("Login", "Login");
            if (!ServiceManager.UserServices.AuthorizeRole(Session["UserName"].ToString(), "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();
        }
    }
}