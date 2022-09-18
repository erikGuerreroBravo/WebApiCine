using Microsoft.EntityFrameworkCore;
using WebApiCine.Entidades;

namespace WebApiCine
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)    
        {

        }
        /// <summary>
        /// utilizamos la API Fluent para que podamos establecer las llaves entre las tablas de muchos a muchos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //establecemos las llaves entre las tablas de muchos a muchos
            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculasId });
            //establecemos 
            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            base.OnModelCreating(modelBuilder); 
        }



        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }

    }
}
