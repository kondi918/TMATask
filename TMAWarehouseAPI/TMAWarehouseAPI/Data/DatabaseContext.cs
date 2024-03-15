using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Models;

namespace TMAWarehouseAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }   
        public DbSet<User> Users { get; set; }
    }
}
