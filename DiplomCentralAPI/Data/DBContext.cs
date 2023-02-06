using DiplomCentralAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomCentralAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)  
        {
        }

        public DbSet<Handler> Handlers;
        public DbSet<Schema> Schemas;
        public DbSet<Experiment> Experiments;
        public DbSet<Photo> Photos;
    }
}
