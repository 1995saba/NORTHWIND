using Northwind.DAL.DAOs.Connected_Mode;
using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = ConfigurationManager
            //      .ConnectionStrings["MyConnectionString"]
            //      .ToString();
            //DataSet dataSet = new DataSet();

            //Console.WriteLine("Введите название страны: ");
            //string country = Console.ReadLine();

            //using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            //{
            //    string ordersSelectCommand = $"SELECT COUNT(Employees.EmployeeID) FROM Employees WHERE COUNTRY = '{country}'";
            //    string employeesSelectCommand = $"SELECT COUNT(Orders.OrderID) FROM Orders WHERE ShipCountry = '{country}'";
            //    string customersSelectCommand=$"SELECT COUNT(Customers.CustomerID) FROM Customers WHERE COUNTRY = '{country}'";
            //    sqlConnection.Open();
            //    using (SqlDataAdapter adapter = new SqlDataAdapter(ordersSelectCommand, connectionString))
            //    {
            //        adapter.Fill(dataSet, "ordersInformation");
            //    }
            //    using (SqlDataAdapter adapter = new SqlDataAdapter(employeesSelectCommand, connectionString))
            //    {
            //        adapter.Fill(dataSet, "employeesInformation");
            //    }
            //    using (SqlDataAdapter adapter = new SqlDataAdapter(customersSelectCommand, connectionString))
            //    {
            //        adapter.Fill(dataSet, "customersInformation");
            //    }
            //    sqlConnection.Close();

            //    Console.WriteLine("Заказов: ", dataSet.Tables["ordersInformation"].Rows.ToString());
            //    Console.WriteLine("Сотрудников: ", dataSet.Tables["employeesInformation"].Rows.ToString());
            //    Console.WriteLine("Клиентов: ", dataSet.Tables["customersInformation"].Rows.ToString());
            //}
            SupplierDTO dto = new SupplierDTO { CompanyName = "asd",
                        ContactName="asd", Country="SabaLand"};

            SupplierDAO dao = new SupplierDAO();
            try
            {
                dao.Create(dto);
                Console.WriteLine("Success");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
