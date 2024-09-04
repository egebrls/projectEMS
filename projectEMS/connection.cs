using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace projectEMS
{
    public class connection
    {
        public static string sqlID = string.Empty;
        public SqlConnection dbBag()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbEMS;Integrated Security=True;");
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

            }
            catch //(Exception e)
            {
                return conn;
            }

            return (conn);
        }
    }
}