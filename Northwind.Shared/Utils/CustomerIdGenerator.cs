using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Shared.Utils
{
    public class CustomerIdGenerator
    {
        public static string GenerateCustomerId(CustomerDTO customerDTO)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                sb.Append(customerDTO.CompanyName[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                sb.Append(customerDTO.ContactName[i]);
            }
            return sb.ToString().ToUpper();
        }
    }
}
