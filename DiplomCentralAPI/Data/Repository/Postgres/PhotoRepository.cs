using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class PhotoRepository : IRepository<Photo>
    {

        private readonly DBContext _DBContext;

        public PhotoRepository(DBContext dbContext) 
        { 
            _DBContext = dbContext;
        }

        public Photo Add(Photo entity)
        {
            _DBContext.Photos.Add(entity);
            return entity;
        }

        public bool Delete(Photo entity)
        {
            _DBContext.Photos.Remove(entity);
            return true;
        }

        public Photo Get(int id) => _DBContext.Photos.Find(id);

        public IEnumerable<Photo> GetAll() => _DBContext.Photos.ToList();

        public async Task<int> SaveChanges() => await _DBContext.SaveChangesAsync();

        public Photo Update(Photo entity)
        {
            _DBContext.Photos.Update(entity);
            return entity;
        }
    }
}
