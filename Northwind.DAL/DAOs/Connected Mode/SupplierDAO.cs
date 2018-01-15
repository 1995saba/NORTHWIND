using Northwind.DAL.Interfaces;
using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DAL.DAOs.Connected_Mode
{
    public class SupplierDAO : IDAO<SupplierDTO>
    {
        private SqlConnection sqlConnection = null;
        public void Create(SupplierDTO t)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseInsertQuery = @"INSERT INTO [NORTHWNDSDP-162].[dbo].[Suppliers] " +
                                     "(SupplierID, CompanyName, ContactName, ContactTitle, " +
                                     "Address, City, Region, PostalCode, Country, Phone, Fax, HomePage) " +
                                     "VALUES (" +
                                     "'{0}', '{1}','{2}','{3},'{4}'," +
                                     "'{5}','{6}','{7}','{8}','{9}'," +
                                     "'{10}','{11}')";
                    string realInsertQuery = String.Format(baseInsertQuery,
                        t.SupplierId,
                        t.CompanyName,
                        t.ContactName,
                        t.ContactTitle,
                        t.Address,
                        t.City,
                        t.Region,
                        t.PostalCode,
                        t.Country,
                        t.Phone,
                        t.Fax,
                        t.HomePage);

                    sqlCommand.CommandText = realInsertQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                sqlConnection.Close();
            }
        }

        public void Delete<U>(ref U id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"DELETE FROM [NORTHWNDSDP-162].[dbo].[Suppliers] " +
                                     "WHERE [SupplierID] = {0}";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                sqlConnection.Close();
            }
        }

        public SupplierDTO Read<U>(ref U id)
        {
            SupplierDTO supplierDTOToReturn = null;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Suppliers] " +
                                     "WHERE [SupplierID] = {0}";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        supplierDTOToReturn = new SupplierDTO()
                        {
                            SupplierId = reader["SupplierID"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            ContactTitle = reader["ContactTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Region = reader["Region"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Country = reader["Country"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString(),
                            HomePage = reader["Homepage"].ToString()
                        };
                    }
                }
                sqlConnection.Close();
            }
            return supplierDTOToReturn;
        }

        public ICollection<SupplierDTO> Read()
        {
            List<SupplierDTO> supplierDTOsToReturn = new List<SupplierDTO>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string realSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Suppliers]";

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            supplierDTOsToReturn.Add(new SupplierDTO()
                            {
                                SupplierId = reader["CustomerID"].ToString(),
                                CompanyName = reader["CompanyName"].ToString(),
                                ContactName = reader["ContactName"].ToString(),
                                ContactTitle = reader["ContactTitle"].ToString(),
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                Region = reader["Region"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                Country = reader["Country"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Fax = reader["Fax"].ToString(),
                                HomePage = reader["HomePage"].ToString()
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return supplierDTOsToReturn;
        }

        public void Update(SupplierDTO t)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseInsertQuery = @"UPDATE [NORTHWNDSDP-162].[dbo].[Suppliers] " +
                                     "SET CompanyName='{1}', ContactName='{2}', ContactTitle='{3}', " +
                                     "Address='{4}', City='{5}', Region='{6}', " +
                                     "PostalCode='{7}', Country='{8}', Phone='{9}', Fax='{10}', HomePage='{11}') " +
                                     "WHERE CustomerID ='{0}'";
                    string realInsertQuery = String.Format(baseInsertQuery,
                        t.SupplierId,
                        t.CompanyName,
                        t.ContactName,
                        t.ContactTitle,
                        t.Address,
                        t.City,
                        t.Region,
                        t.PostalCode,
                        t.Country,
                        t.Phone,
                        t.Fax,
                        t.HomePage);

                    sqlCommand.CommandText = realInsertQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                sqlConnection.Close();
            }
        }
    }
}
