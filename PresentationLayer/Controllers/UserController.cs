using BusinnesLayer;
using PresentationLayer.Models;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer.Controllers
{
    public class UserController : ControllerExtends
    {      
        public JsonResult GetListUser()
        {
            try{
                if (Session["UserName"] == null) Response.Redirect("/Login/Login");
                int idUser = Convert.ToInt32(Session["IdUser"]);
                if (!ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Not authorized to access" };
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                List<User> list = ServiceManager.UserServices.ListUsers();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Error "+e.Message };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult CreateUser(User user)
        {
            try
            {
                if (Session["UserName"] == null) Response.Redirect("/Login/Login");
                int idUser = Convert.ToInt32(Session["IdUser"]);
                if (!ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Not authorized to access" };
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                CreateUserModel message = new CreateUserModel();

                User userAct = ServiceManager.UserServices.GetUser(user.UserName);
                if (userAct== null)
                {
                    int id = ServiceManager.UserServices.CreateUser(user);
                    message.IdUser = id;
                    message.Message = "Save user";
                }
                else
                {
                    message.IdUser = 0;
                    message.Message = "The user already exists in the system";
                }              
                return Json(message, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Error " + e.Message };
                return Json(msg, JsonRequestBehavior.DenyGet);
            }

        }
       
        [HttpPut]
        public JsonResult UpdateUser(User user)
        {
            try
            {
                if (Session["UserName"] == null) Response.Redirect("/Login/Login");
                int idUser = Convert.ToInt32(Session["IdUser"]);
                if (!ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Not authorized to access" };
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                CreateUserModel message = new CreateUserModel();

                User userAct = ServiceManager.UserServices.GetUser(user.UserName);
                if (userAct == null)
                {
                    message.IdUser = 0;
                    message.Message = "The user not exists in the system";
                }
                else
                {
                    bool isCorrect = ServiceManager.UserServices.UpdateUser(user);
                    message.IdUser = 0;
                    message.Message = "Save user";
                }
                return Json(message, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Error " + e.Message };
                return Json(msg, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpDelete]
        public JsonResult DeleteUser(string userName)
        {
            try
            {
                if (Session["UserName"] == null) Response.Redirect("/Login/Login");
                int idUser = Convert.ToInt32(Session["IdUser"]);
                if (!ServiceManager.UserServices.AuthorizeRole(idUser, "ADMIN"))
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Not authorized to access" };
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                CreateUserModel message = new CreateUserModel();

                User userAct = ServiceManager.UserServices.GetUser(userName);
                if (userAct == null)
                {
                    message.IdUser = 0;
                    message.Message = "The user not exists in the system";
                }
                else
                {
                    bool isCorrect = ServiceManager.UserServices.DeleteUser(userName);
                    message.IdUser = 0;
                    message.Message = "Delete user";
                }
                return Json(message, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Error " + e.Message };
                return Json(msg, JsonRequestBehavior.DenyGet);
            }

        }


    }
}
