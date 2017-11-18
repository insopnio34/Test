using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PercistenceLayer.API
{
    public interface IConnectionBD
    {
        SqlConnection OpenDB();
        void CloseDB(SqlConnection cnx);

    }
}
