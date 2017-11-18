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
        int CreateUser(User user);
        bool UpdateUser(User user);

        bool DeleteUser(string userName);

        bool AddRole(int idUser, int idRole);

        bool DeleteRoles(int idUser);

        bool AuthorizeRole(int idUser,string role);

        List<Role> GetListRole(int idUser);

        List<User> ListUsers();

    }
}
