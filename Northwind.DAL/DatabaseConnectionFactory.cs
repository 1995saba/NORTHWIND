using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DAL
{
    public class DatabaseConnectionFactory
    {
        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["MyConnectionStringOne"]
                    .ToString();
            }
        }
        public static SqlConnection GetConnection()
        {
            SqlConnection dbConnection = new SqlConnection(ConnectionString);
            return dbConnection;
        }
    }
}
