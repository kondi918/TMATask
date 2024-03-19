using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Models;

namespace TMAWarehouseAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }   
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<TMARequest> TMARequests { get; set; }
        public DbSet<TMARequestRows> TMARequestRows { get; set;}
    }
}
