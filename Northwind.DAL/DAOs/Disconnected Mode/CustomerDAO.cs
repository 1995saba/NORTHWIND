using Northwind.DAL.Interfaces;
using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DAL.DAOs.Disconnected_Mode
{
    public class CustomerDAO : IDAO<CustomerDTO>
    {
        string connectionString = ConfigurationManager
                  .ConnectionStrings["MyConnectionString"]
                  .ToString();
        DataSet dataSet = new DataSet();

        string sql = "SELECT * FROM Customers";

        public void Create(CustomerDTO dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    DataRow dataRowToAdd = dataSet.Tables["Customers"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["Customers"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["Customers"]);
                }
                sqlConnection.Close();
            }
        }

        public void Delete<U>(ref U id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow toDelete = dataSet.Tables[0].Rows.Find(id);

                    toDelete.Delete();
                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }

        public CustomerDTO Read<U>(ref U id)
        {
            CustomerDTO dtoToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");
                    dataSet.Tables["Customers"].PrimaryKey = new DataColumn[] { dataSet.Tables["Customers"].Columns["CustomersID"] };

                    DataRow dataRow = dataSet.Tables["Customers"].Rows.Find(dtoToReturn.CustomerId);

                    dtoToReturn = new CustomerDTO
                    {
                        CustomerId = dataRow["CustomerID"].ToString(),
                        CompanyName = dataRow["CompanyName"].ToString(),
                        ContactName = dataRow["ContactName"].ToString(),
                        ContactTitle = dataRow["ContactTitle"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Region = dataRow["Region"].ToString(),
                        PostalCode = dataRow["PostalCode"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Fax = dataRow["Fax"].ToString()
                    };
                }
                sqlConnection.Close();
            }
            return dtoToReturn;
        }

        public ICollection<CustomerDTO> Read()
        {
            List<CustomerDTO> dtosToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");
                    
                }

                foreach (DataRow dataRow in dataSet.Tables["Customers"].Rows)
                {
                    dtosToReturn.Add(new CustomerDTO
                    {
                        CustomerId = dataRow["CustomerID"].ToString(),
                        CompanyName = dataRow["CompanyName"].ToString(),
                        ContactName = dataRow["ContactName"].ToString(),
                        ContactTitle = dataRow["ContactTitle"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Region = dataRow["Region"].ToString(),
                        PostalCode = dataRow["PostalCode"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Fax = dataRow["Fax"].ToString()
                    });
                }

                sqlConnection.Close();
            }
            return dtosToReturn;
        }

        public void Update(CustomerDTO dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow dataRow = dataSet.Tables[0].Rows.Find(dto.CustomerId);

                    dataRow.BeginEdit();

                    dataRow["CustomerID"] = dto.CustomerId;
                    dataRow["CompanyName"] = dto.CompanyName;
                    dataRow["ContactName"] = dto.ContactName;
                    dataRow["ContactTitle"] = dto.ContactTitle;
                    dataRow["Address"] = dto.Address;
                    dataRow["City"] = dto.City;
                    dataRow["Region"] = dto.Region;
                    dataRow["PostalCode"] = dto.PostalCode;
                    dataRow["Country"] = dto.Country;
                    dataRow["Phone"] = dto.Phone;
                    dataRow["Fax"] = dto.Fax;

                    dataRow.EndEdit();
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }
    }
}
