using BusinnesLayer;
using PercistenceLayer.IMPL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Establecer conexión");
            ConnectionBD bd =new ConnectionBD();
            SqlConnection cnx=bd.OpenDB();
            Console.WriteLine("ConnectionString: {0}",
            cnx.ConnectionString);
            bd.CloseDB(cnx);

            Console.WriteLine("Crear usuario");
            UserDAO userDao = new UserDAO();
            User user = new User();
            Console.WriteLine("Nombre usuario");
            user.UserName = Console.ReadLine();
            Console.WriteLine("Password");
            user.Password = Console.ReadLine();
            user.RolesList.Add("ADMIN");
            userDao.CreateUser(user);

            Console.WriteLine("Leer usuarios");
            List<User> users = userDao.ListUsers();
            foreach(User itemUser in users)
            {
                Console.WriteLine("User:"+itemUser.UserName+" - ");
            }

            Console.WriteLine("Seleccionar cualquier tecla para acabar");
            Console.Read();









        }
    }
}
