using System.ComponentModel.DataAnnotations;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class PeliculaCreacionDto: PeliculaPatchDto
    {
        
        [SizeImagenValidacion(pesoMaximoMB: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }
        //Ids Asociados de los generos a las peliculas
        public List<int> GenerosIds { get; set; }


    }
}
