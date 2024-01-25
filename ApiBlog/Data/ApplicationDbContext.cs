using ApiBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Agreagar los modelos
        public DbSet<Post> Post { get; set; }
        public DbSet<User> User { get; set; }   
    }
}
