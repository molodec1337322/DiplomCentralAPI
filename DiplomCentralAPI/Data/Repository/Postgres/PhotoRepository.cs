using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class PhotoRepository : IRepository<Photo>
    {
        public Photo Add(Photo entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Photo entity)
        {
            throw new NotImplementedException();
        }

        public Photo Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Photo Update(Photo entity)
        {
            throw new NotImplementedException();
        }
    }
}
