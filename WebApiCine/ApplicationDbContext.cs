using Microsoft.EntityFrameworkCore;

namespace WebApiCine
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)    
        {

        }

        public DbSet<Genero> Generos { get; set; }
    }
}
