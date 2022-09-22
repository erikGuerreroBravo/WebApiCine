namespace WebApiCine.DTO
{
    public class PeliculaDetallesDto
    {
        public List<GeneroDto> Generos { get; set; }
        public List<ActorPeliculaDetalleDto> Actores { get; set; }
    }
}
