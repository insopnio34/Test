﻿using System;
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
            if (Session["UserName"] == null) return RedirectToAction("Login", "Login");
            if (!ServiceManager.UserServices.AuthorizeRole(Session["UserName"].ToString(), "PAGE_2") && !ServiceManager.UserServices.AuthorizeRole(Session["UserName"].ToString(), "ADMIN"))
            {
                return RedirectToAction("NotAuthorized", "Login");
            }
            return View();
        }
    }
}