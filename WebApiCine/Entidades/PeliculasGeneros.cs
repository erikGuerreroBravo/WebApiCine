namespace WebApiCine.Entidades
{
    public class PeliculasGeneros
    {
        public int GeneroId { get; set; }
        public int PeliculaId { get; set; }

        //establecemos las propiedades de navegacion
        public Genero Genero { get; set; }

        public Pelicula Pelicula { get; set; }


    }
}
