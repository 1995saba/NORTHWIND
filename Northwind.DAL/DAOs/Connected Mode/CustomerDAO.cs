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
    public class CustomerDAO : IDAO<CustomerDTO>
    {
        private SqlConnection sqlConnection = null;
        public void Create(CustomerDTO t)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseInsertQuery = @"INSERT INTO [NORTHWNDSDP-162].[dbo].[Customers] " +
                                     "(CustomerID, CompanyName, ContactName, ContactTitle, " +
                                     "Address, City, Region, PostalCode, Country, Phone, Fax) " +
                                     "VALUES (" +
                                     "'{0}', '{1}','{2}','{3},'{4}'," +
                                     "'{5}','{6}','{7}','{8}','{9}'," +
                                     "'{10}')";
                    string realInsertQuery = String.Format(baseInsertQuery,
                        t.CustomerId,
                        t.CompanyName,
                        t.ContactName,
                        t.ContactTitle,
                        t.Address,
                        t.City,
                        t.Region,
                        t.PostalCode,
                        t.Country,
                        t.Phone,
                        t.Fax);

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
                    string baseSelectQuery = @"DELETE FROM [NORTHWNDSDP-162].[dbo].[Customers] " +
                                     "WHERE [CustomerID] = {0}";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                sqlConnection.Close();
            }
        }

        public CustomerDTO Read<U>(ref U id)
        {
            CustomerDTO customerDTOToReturn = null;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Customers] " +
                                     "WHERE [CustomerID] = {0}";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        customerDTOToReturn = new CustomerDTO()
                        {
                            CustomerId = reader["CustomerID"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            ContactTitle = reader["ContactTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Region = reader["Region"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Country = reader["Country"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString()
                        };
                    }
                }
                sqlConnection.Close();
            }
            return customerDTOToReturn;
        }

        public ICollection<CustomerDTO> Read()
        {
            List<CustomerDTO> customerDTOsToReturn = new List<CustomerDTO>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string realSelectQuery = @"SELECT * FROM [NORTHWNDSDP-162].[dbo].[Customers]";

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerDTOsToReturn.Add(new CustomerDTO()
                            {
                                CustomerId = reader["CustomerID"].ToString(),
                                CompanyName = reader["CompanyName"].ToString(),
                                ContactName = reader["ContactName"].ToString(),
                                ContactTitle = reader["ContactTitle"].ToString(),
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                Region = reader["Region"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                Country = reader["Country"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Fax = reader["Fax"].ToString()
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return customerDTOsToReturn;
        }

        public void Update(CustomerDTO t)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseInsertQuery = @"UPDATE [NORTHWNDSDP-162].[dbo].[Customers] " +
                                     "SET CompanyName='{1}', ContactName='{2}', ContactTitle='{3}', " +
                                     "Address='{4}', City='{5}', Region='{6}', " +
                                     "PostalCode='{7}', Country='{8}', Phone='{9}', Fax='{10}') " +
                                     "WHERE CustomerID ='{0}'";
                    string realInsertQuery = String.Format(baseInsertQuery,
                        t.CustomerId,
                        t.CompanyName,
                        t.ContactName,
                        t.ContactTitle,
                        t.Address,
                        t.City,
                        t.Region,
                        t.PostalCode,
                        t.Country,
                        t.Phone,
                        t.Fax);

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
