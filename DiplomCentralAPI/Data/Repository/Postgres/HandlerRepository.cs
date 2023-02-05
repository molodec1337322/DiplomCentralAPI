using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class HandlerRepository : IRepository<Handler>
    {
        public Handler Add(Handler entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Handler entity)
        {
            throw new NotImplementedException();
        }

        public Handler Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Handler> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Handler Update(Handler entity)
        {
            throw new NotImplementedException();
        }
    }
}
