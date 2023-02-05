using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class SchemaRepository : IRepository<Schema>
    {
        public Schema Add(Schema entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Schema entity)
        {
            throw new NotImplementedException();
        }

        public Schema Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schema> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Schema Update(Schema entity)
        {
            throw new NotImplementedException();
        }
    }
}
