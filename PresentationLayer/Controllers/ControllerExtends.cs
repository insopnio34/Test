using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class ControllerExtends : Controller
    {
        
        private readonly ServiceManager serviceManager = new ServiceManager();
        protected ServiceManager ServiceManager
        {
            get { return serviceManager; }

        }     
        protected override void Dispose(bool disposing)
        {
            //eliminamos nuestros recursos
            if (disposing)
            {
                this.serviceManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}