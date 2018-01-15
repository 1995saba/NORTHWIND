using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DAL.Interfaces
{
    public interface IDAO<T> where
        T : class
    {
        void Create(T t);
        void Update(T t);
        void Delete<U>(ref U id);
        T Read<U>(ref U id);
        ICollection<T> Read();
    }
}
