using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Data.Repository.Postgres
{
    public class ExperimentRepository : IRepository<Experiment>
    {
        public Experiment Add(Experiment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Experiment entity)
        {
            throw new NotImplementedException();
        }

        public Experiment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Experiment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Experiment Update(Experiment entity)
        {
            throw new NotImplementedException();
        }
    }
}
