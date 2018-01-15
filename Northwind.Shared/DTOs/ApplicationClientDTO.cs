using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Shared.DTOs
{
    public abstract class ApplicationClientDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        protected string GetDetailedTypeInfo()
        {
            StringBuilder sb = new StringBuilder();
            Type currentType = GetType();
            PropertyInfo[] properties = currentType.GetProperties();

            sb.AppendLine(currentType.Name);
            foreach (var item in properties)
            {
                sb.AppendLine($"{item.Name} - {item.GetValue(this, null)}");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return $"{GetDetailedTypeInfo()}";
        }
    }
}
