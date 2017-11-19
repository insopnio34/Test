using BusinnesLayer;
using PresentationLayer.Models;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer.Controllers
{
    public class LoginController : ControllerExtends
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    User user = ServiceManager.UserServices.GetUser(model.UserName, model.Password);
                    if (user != null)
                    { 
                        string role = user.RolesList.First();

                        Session["UserName"] = user.UserName;
                        switch (role)
                        {
                            case "ADMIN":
                                return RedirectToAction("PruebaJson", "PAGE_1");
                            case "PAGE_1":                            
                                return RedirectToAction("PAGE_1", "PAGE_1");
                            case "PAGE_2":                              
                                return RedirectToAction("PAGE_2", "PAGE_2");
                            case "PAGE_3":
                                return RedirectToAction("PAGE_3", "PAGE_3");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos.");
                    }
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", "En estos momentos no se puede accerder al sistema, error: "+e.Message);
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["IdUser"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}