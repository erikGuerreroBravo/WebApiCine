using Microsoft.EntityFrameworkCore;
using WebApiCine.Entidades;

namespace WebApiCine
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)    
        {

        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
    }
}
