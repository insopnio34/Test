using BusinnesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PercistenceLayer.API
{
    public interface IUserDAO
    {
        User GetUser(string userName,string password);
        User GetUser(string userName);
        bool CreateUser(User user);
        bool UpdateUser(User user);

        bool DeleteUser(string userName);

        bool AddRole(string userName, string role);

        bool DeleteRoles(string userName);

        bool AuthorizeRole(string userName, string role);

        List<string> GetListRole(string userName);

        List<User> ListUsers();

    }
}
