using DiplomCentralAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomCentralAPI.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)  
        {
            //Database.EnsureCreated();
        }

        public DbSet<MyHandler> MyHandlers { get; set; }
        public DbSet<Schema> Schemas { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
