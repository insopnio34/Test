﻿using ServicesLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinnesLayer;
using PercistenceLayer.IMPL;

namespace ServicesLayer.IMPL
{
    public class UserServices : IUserServices
    {
        public bool AuthorizeRole(int idUser, string role)
        {
            UserDAO userDao = new UserDAO();
            return userDao.AuthorizeRole(idUser, role);
        }

        public int CreateUser(User user)
        { 
            UserDAO userDao = new UserDAO();
            return userDao.CreateUser(user);
        }

        public bool DeleteUser(string userName)
        {
            UserDAO userDao = new UserDAO();
            return userDao.DeleteUser(userName);
        }

        public User GetUser(string userName)
        {
            UserDAO userDao = new UserDAO();
            return userDao.GetUser(userName);
        }

        public User GetUser(string userName, string password)
        {
            UserDAO userDao = new UserDAO();
            return userDao.GetUser(userName,password);
        }

        public List<User> ListUsers()
        {
            UserDAO userDao = new UserDAO();
            return userDao.ListUsers();
        }

        public bool UpdateUser(User user)
        {
            UserDAO userDao = new UserDAO();
            return userDao.UpdateUser(user);
        }
    }
}
