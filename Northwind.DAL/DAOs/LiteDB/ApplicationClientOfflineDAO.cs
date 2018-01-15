using LiteDB;
using Northwind.DAL.Interfaces;
using Northwind.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DAL.DAOs.LiteDB
{
    public class ApplicationClientOfflineDAO : IDAO<ApplicationClientDTO>
    {
        private string dbToStorePath;
        public void Create(ApplicationClientDTO record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<ApplicationClientDTO>("app_client_dtos");
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
        }

        public void Delete<U>(ref U id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<ApplicationClientDTO>("app_client_dtos");
                col.Delete(id.ToString());
            }
        }

        public ApplicationClientDTO Read<U>(ref U id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<ApplicationClientDTO>("app_client_dtos");
                return col.FindById(id.ToString());
            }
        }

        public ICollection<ApplicationClientDTO> Read()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<ApplicationClientDTO>("app_client_dtos");
                return col.FindAll().ToList();
            }
        }

        public void Update(ApplicationClientDTO record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<ApplicationClientDTO>("app_client_dtos");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
        }
    }
}
