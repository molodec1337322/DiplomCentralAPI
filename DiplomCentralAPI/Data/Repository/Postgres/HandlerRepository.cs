using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class HandlerRepository : IRepository<Handler>
    {

        private readonly MyDBContext _DBContext;

        public HandlerRepository(MyDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public Handler Add(Handler entity)
        {
            _DBContext.Handlers.Add(entity);
            return entity;
        }

        public bool Delete(Handler entity)
        {
            _DBContext.Handlers.Remove(entity);
            return true;
        }

        public Handler Get(int id) => _DBContext.Handlers.Find(id);

        public IEnumerable<Handler> GetAll() => _DBContext.Handlers.ToList();

        public async Task<int> SaveChanges() => await _DBContext.SaveChangesAsync();

        public Handler Update(Handler entity)
        {
            _DBContext.Handlers.Update(entity);
            return entity;
        }
    }
}
