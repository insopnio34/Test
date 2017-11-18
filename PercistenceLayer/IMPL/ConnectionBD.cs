using PercistenceLayer.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PercistenceLayer.IMPL
{
    public class ConnectionBD:IConnectionBD
    {
        string cnnString;
        public ConnectionBD()
        {
            cnnString = ConfigurationManager.ConnectionStrings["HabitacliaConnection"].ConnectionString;
        }
        public SqlConnection OpenDB()
        {
            try
            {
                SqlConnection cnx = new SqlConnection(cnnString);
                cnx.Open();
                return cnx;
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public void CloseDB(SqlConnection cnx)
        {
            try
            {
                cnx.Close();
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

    }
}
