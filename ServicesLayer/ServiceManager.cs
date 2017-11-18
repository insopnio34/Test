using ServicesLayer.API;
using ServicesLayer.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ServiceManager : IDisposable
    {
        IUserServices userServices;
        public void Dispose()
        {
           
        }
        public IUserServices UserServices
        {
            get { return userServices ?? (userServices = new UserServices()); }
        }
    }
}
