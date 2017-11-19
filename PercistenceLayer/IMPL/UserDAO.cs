using PercistenceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinnesLayer;
using System.Data.SqlClient;

namespace PercistenceLayer.IMPL
{
    public class UserDAO : IUserDAO
    {
        private User GetUser(SqlDataReader rs)
        {
            User user = null;
            try
            {
                user = new User();
                user.UserName = rs["UserName"].ToString();
                user.RolesList = GetListRole(user.UserName);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return user;
        }

        public User GetUser(string userName,string password)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            User user=null;
            try
            {
                string query = "Select * from  [dbo].[Users] Where UserName=@UserName and Password=@Password";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader rs=cmd.ExecuteReader();
                if (rs.Read())
                {
                    user = GetUser(rs);                   
                }
                return user;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }

        }

        public User GetUser(string userName)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            User user = null;
            try
            {
                string query = "Select * from  [dbo].[Users] Where UserName=@UserName";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    user = GetUser(rs);
                }
                return user;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }

        }

        public List<User> ListUsers()
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            List<User> users = null;
            try
            {
                string query = "Select * from  [dbo].[Users]";
                SqlCommand cmd = new SqlCommand(query, cnx);
                SqlDataReader rs = cmd.ExecuteReader();
                users = new List<User>();
                User user;
                    while (rs.Read())
                    {
                        user = GetUser(rs);
                        users.Add(user);
                    }
                                 
                return users;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }

        }

        public bool CreateUser(User user)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isCreate=false;
            try
            {             
                string query = "INSERT INTO [dbo].[Users](UserName,Password) VALUES(@UserName,@Password)";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                if (user.RolesList!=null && user.RolesList.Count > 0)
                {
                    foreach(string role in user.RolesList)
                    {
                        AddRole(user.UserName,role);
                    }
                }
                isCreate = true;
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally{
                db.CloseDB(cnx);
            }
            return isCreate;
        }

        public bool UpdateUser(User user)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isSave = false;
            try
            {
                string query = "UPDATE [dbo].[Users] SET Password = @Password WHERE UserName=@UserName";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                if (user.RolesList != null && user.RolesList.Count > 0)
                {
                        DeleteRoles(user.UserName);
                        foreach (string role in user.RolesList)
                        {
                            AddRole(user.UserName, role);
                        }
                }
                    isSave = true;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return isSave;
        }
        public bool DeleteUser(string userName)
        {        
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isDelete = false;
            try
            {
                string query = "DELETE FROM [dbo].[Users] WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);            
                cmd.ExecuteNonQuery();
                isDelete=true;                
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return isDelete;
        }
        public bool AddRole(string userName, string role)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isSave = false;
            try
            {
                string query = "INSERT INTO [dbo].[UsersAndRoles](Role,UserName) VALUES(@Role,@UserName)";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.ExecuteNonQuery();
                isSave = true;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return isSave;
        }
        public bool DeleteRoles(string userName)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isDelete = false;
            try
            {
                string query = "DELETE FROM [dbo].[UsersAndRoles] WHERE UserName=@UserName";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.ExecuteNonQuery();
                isDelete = true;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return isDelete;
        }
        public bool AuthorizeRole(string userName, string role)
        {
            bool isValid = false;
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * from [dbo].[UsersAndRoles] ");
                query.Append("where UserName = @UserName and Role =@Role ");
                SqlCommand cmd = new SqlCommand(query.ToString(), cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Role", role);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {                   
                    isValid = true;
                }              
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return isValid;
        }
        public List<string> GetListRole(string userName)
        {      
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            List<string> listRoles = null;
            try
            {
                string query = "SELECT * from [dbo].[UsersAndRoles]  where UserName = @UserName";                  
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", userName);
                SqlDataReader rs = cmd.ExecuteReader();
                listRoles = new List<string>();

                while (rs.Read())
                {
                    listRoles.Add(rs["Role"].ToString());
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                db.CloseDB(cnx);
            }
            return listRoles;
        }
        
    }
}
