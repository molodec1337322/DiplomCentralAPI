using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class ExperimentRepository : IRepository<Experiment>
    {

        private readonly MyDBContext _DBContext;

        public ExperimentRepository(MyDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public Experiment Add(Experiment entity)
        {
            _DBContext.Experiments.Add(entity);
            return entity;
        }

        public bool Delete(Experiment entity)
        {
            _DBContext.Experiments.Remove(entity);
            return true;
        }

        public Experiment Get(int id) => _DBContext.Experiments.Find(id);

        public IEnumerable<Experiment> GetAll() => _DBContext.Experiments.ToList();

        public async Task<int> SaveChanges() => await _DBContext.SaveChangesAsync();

        public Experiment Update(Experiment entity)
        {
            _DBContext.Experiments.Update(entity);
            return entity;
        }
    }
}
