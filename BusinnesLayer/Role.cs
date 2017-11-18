using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnesLayer
{
    public class Role
    {
        #region atributos
        int _IdRole;
        string _RoleName;
        #endregion
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        public int IdRole
        {
            get { return _IdRole; }
            set { _IdRole = value; }
        }

       public Role() { }

        public Role(int idRole,string roleName)
        {
            _IdRole = idRole;
            _RoleName = roleName;
        }
    }
}
