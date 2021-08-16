using Microsoft.EntityFrameworkCore;

namespace EFDemo.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Game> Games {get; set;}
    }
}