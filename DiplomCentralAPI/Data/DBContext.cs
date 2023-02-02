using Microsoft.EntityFrameworkCore;

namespace DiplomCentralAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)  
        {
        }
    }
}
