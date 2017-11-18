using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnesLayer
{
    public class User
    {
        #region atributos
        int _IdUser;
        string _UserName;
        string _Password;
        List<Role> _RolesList;
        #endregion
        #region Metodos Publicos
        public int IdUser
        {
            get { return _IdUser; }
            set { _IdUser = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public List<Role> RolesList
        {
            get { return _RolesList; }
            set { _RolesList = value; }
        }

        #endregion

        #region constructor
        public User()
        {
            _RolesList = new List<Role>();
        }
        #endregion
    }
}
