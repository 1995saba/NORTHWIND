using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Shared.Utils
{
    public class SupplierIdGenerator
    {
        public int GenerateSupplierId()
        {
            int newId = GetLastId() + 1;
            return newId;
        }

        public int GetLastId()
        {
            int lastId;
            string connectionString = ConfigurationManager
                  .ConnectionStrings["MyConnectionString"]
                  .ToString();
            DataSet dataSet = new DataSet();

            string sql = "SELECT * FROM Suppliers" +
                "ORDER BY SupplierID DESC";
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    adapter.Fill(dataSet, "Suppliers");

                }

                DataRow row = dataSet.Tables["Suppliers"].Rows[0];
                bool res = Int32.TryParse(row["SupplierID"].ToString(), out lastId);

                sqlConnection.Close();
            }
            return lastId;
        }
    }
}
