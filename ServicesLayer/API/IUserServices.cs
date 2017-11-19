using BusinnesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.API
{
    public interface IUserServices
    {
        User GetUser(string userName, string password);
        User GetUser(string userName);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(string userName);
        List<User> ListUsers();
        bool AuthorizeRole(string userName, string role);
    }
}
