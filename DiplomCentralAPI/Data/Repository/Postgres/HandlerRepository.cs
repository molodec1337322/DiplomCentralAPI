using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class HandlerRepository : IRepository<MyHandler>
    {

        private readonly MyDBContext _DBContext;

        public HandlerRepository(MyDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public MyHandler Add(MyHandler entity)
        {
            _DBContext.MyHandlers.Add(entity);
            return entity;
        }

        public bool Delete(MyHandler entity)
        {
            _DBContext.MyHandlers.Remove(entity);
            return true;
        }

        public MyHandler Get(int id) => _DBContext.MyHandlers.Find(id);

        public IEnumerable<MyHandler> GetAll() => _DBContext.MyHandlers.ToList();

        public async Task<int> SaveChanges() => await _DBContext.SaveChangesAsync();

        public MyHandler Update(MyHandler entity)
        {
            _DBContext.MyHandlers.Update(entity);
            return entity;
        }
    }
}
