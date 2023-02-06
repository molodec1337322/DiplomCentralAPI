using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class SchemaRepository : IRepository<Schema>
    {
        private readonly DBContext _DBContext;

        public SchemaRepository(DBContext dbContext)
        {
            _DBContext = dbContext;
        }


        public Schema Add(Schema entity)
        {
            _DBContext.Schemas.Add(entity);
            return entity;
        }

        public bool Delete(Schema entity)
        {
            _DBContext.Schemas.Remove(entity);
            return true;
        }

        public Schema Get(int id) => _DBContext.Schemas.Find(id);

        public IEnumerable<Schema> GetAll() => _DBContext.Schemas.ToList();

        public async Task<int> SaveChanges() => await _DBContext.SaveChangesAsync();

        public Schema Update(Schema entity)
        {
            _DBContext.Schemas.Update(entity);
            return entity;
        }
    }
}
