using Northwind.DAL.Interfaces;
using Northwind.Shared.DTOs;
using Northwind.Shared.Utils;
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
    public class SupplierDAO : IDAO<SupplierDTO>
    {
        string connectionString = ConfigurationManager
                  .ConnectionStrings["MyConnectionString"]
                  .ToString();
        DataSet dataSet = new DataSet();

        string sql = "SELECT * FROM Suppliers";

        public void Create(SupplierDTO dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Suppliers");

                    DataRow dataRowToAdd = dataSet.Tables["Suppliers"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        if (item.Name == "SupplierID")
                        {   SupplierIdGenerator generator = new SupplierIdGenerator();
                            dto.SupplierId = generator.GenerateSupplierId().ToString();
                            dataRowToAdd[item.Name] = dto.SupplierId;
                        }
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["Suppliers"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["Suppliers"]);
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

        public SupplierDTO Read<U>(ref U id)
        {
            SupplierDTO dtoToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Suppliers");
                    dataSet.Tables["Suppliers"].PrimaryKey = new DataColumn[] { dataSet.Tables["Suppliers"].Columns["SupplierID"] };

                    DataRow dataRow = dataSet.Tables["Suppliers"].Rows.Find(dtoToReturn.SupplierId);

                    dtoToReturn = new SupplierDTO
                    {
                        SupplierId = dataRow["SupplierID"].ToString(),
                        CompanyName = dataRow["CompanyName"].ToString(),
                        ContactName = dataRow["ContactName"].ToString(),
                        ContactTitle = dataRow["ContactTitle"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Region = dataRow["Region"].ToString(),
                        PostalCode = dataRow["PostalCode"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Fax = dataRow["Fax"].ToString(),
                        HomePage= dataRow["HomePage"].ToString()
                    };
                }
                sqlConnection.Close();
            }
            return dtoToReturn;
        }

        public ICollection<SupplierDTO> Read()
        {
            List<SupplierDTO> dtosToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Suppliers");
                }

                foreach (DataRow dataRow in dataSet.Tables["Suppliers"].Rows)
                {
                    dtosToReturn.Add(new SupplierDTO
                    {
                        SupplierId = dataRow["CustomerID"].ToString(),
                        CompanyName = dataRow["CompanyName"].ToString(),
                        ContactName = dataRow["ContactName"].ToString(),
                        ContactTitle = dataRow["ContactTitle"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Region = dataRow["Region"].ToString(),
                        PostalCode = dataRow["PostalCode"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Fax = dataRow["Fax"].ToString(),
                        HomePage = dataRow["HomePage"].ToString()
                    });
                }

                sqlConnection.Close();
            }
            return dtosToReturn;
        }

        public void Update(SupplierDTO dto)
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

                    DataRow dataRow = dataSet.Tables[0].Rows.Find(dto.SupplierId);

                    dataRow.BeginEdit();

                    dataRow["SupplierID"] = dto.SupplierId;
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
                    dataRow["HomePage"] = dto.HomePage;

                    dataRow.EndEdit();
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }
    }
}
