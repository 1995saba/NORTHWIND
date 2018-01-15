using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Shared.DTOs
{
    public class SupplierDTO:ApplicationClientDTO
    {
        public string SupplierId { get; set; }
        public string HomePage { get; set; }

        public override string ToString()
        {
            return GetDetailedTypeInfo();
        }
    }
}