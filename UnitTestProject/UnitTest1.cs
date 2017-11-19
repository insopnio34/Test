using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicesLayer;
using BusinnesLayer;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void crearUsuario()
        {
            ServiceManager servicesManager = new ServiceManager();
            User user = new User();
            user.UserName = "Prueba "+DateTime.Now.ToString("hh-mm-ss");
            user.Password = "1234";
            user.RolesList.Add("PAGE_1");
            var result = servicesManager.UserServices.CreateUser(user);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ModificarUsuario()
        {
            ServiceManager servicesManager = new ServiceManager();
            User user = new User();
            user.UserName = "Prueba 1";
            user.Password = "1234";
            user.RolesList.Add("PAGE_1");
            user.RolesList.Add("PAGE_2");
            var result = servicesManager.UserServices.UpdateUser(user);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteUsuario()
        {
            ServiceManager servicesManager = new ServiceManager();
            var result = servicesManager.UserServices.DeleteUser("Casa 1");
            Assert.AreEqual(true, result);
        }
    }
}
