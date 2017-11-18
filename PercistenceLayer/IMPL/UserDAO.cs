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
                user.IdUser = Convert.ToInt32(rs["IdUser"].ToString());
                user.UserName = rs["UserName"].ToString();
                user.RolesList = GetListRole(user.IdUser);
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

        public int CreateUser(User user)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            int idUser = 0;
            try
            {             
                string query = "INSERT INTO [dbo].[Users](UserName,Password) VALUES(@UserName,@Password);SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                string insertedID = cmd.ExecuteScalar().ToString();
                idUser = Convert.ToInt32(insertedID);
                if(user.RolesList!=null && user.RolesList.Count > 0)
                {
                    foreach(Role role in user.RolesList)
                    {
                        AddRole(idUser,role.IdRole );
                    }
                }                            
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally{
                db.CloseDB(cnx);
            }
            return idUser;
        }

        public bool UpdateUser(User user)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isSave = false;
            try
            {
                string query = "UPDATE [dbo].[Users] SET Password = @Password WHERE UserName=@Username";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                if (user.RolesList != null && user.RolesList.Count > 0)
                {
                        DeleteRoles(user.IdUser);
                        foreach (Role role in user.RolesList)
                        {
                            AddRole(user.IdUser, role.IdRole);
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
                string query = "DELETE FROM [dbo].[Users] WHERE UserName = @Username";
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
        public bool AddRole(int idUser, int idRole)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isSave = false;
            try
            {
                string query = "INSERT INTO [dbo].[UsersAndRoles](IdRole,IdUser) VALUES(@IdRole,@IdUser)";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@IdRole", idRole);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
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
        public bool DeleteRoles(int idUser)
        {
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            bool isDelete = false;
            try
            {
                string query = "DELETE FROM [dbo].[Roles] WHERE IdUser=@IdUser";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
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
        public bool AuthorizeRole(int idUser, string role)
        {
            bool isValid = false;
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT role.IdRole from [dbo].[Users] users, ");
                query.Append("[dbo].[Roles] role, [dbo].[UsersAndRoles] userAndRole ");
                query.Append("where role.IdRole = userAndRole.IdRole ");
                query.Append("and users.IdUser = userAndRole.IdUser ");
                query.Append("and users.IdUser = @IdUser and role.Role =@Role");
                SqlCommand cmd = new SqlCommand(query.ToString(), cnx);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
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
        public List<Role> GetListRole(int idUser)
        {      
            ConnectionBD db = new ConnectionBD();
            SqlConnection cnx = db.OpenDB();
            List<Role> listRoles = null;
            try
            {
                string query = "SELECT role.IdRole, role.Role FROM [dbo].[Roles] role,[dbo].[UsersAndRoles] userAndRole where role.IdRole = userAndRole.IdRole and userAndRole.IdUser = @IdUser";                  
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
                SqlDataReader rs = cmd.ExecuteReader();
                listRoles = new List<Role>();
                Role role;
                while (rs.Read())
                {
                    role = new Role();
                    role.IdRole = Convert.ToInt32(rs["IdRole"].ToString());
                    role.RoleName = rs["Role"].ToString();
                    listRoles.Add(role);
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
