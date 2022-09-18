using System.ComponentModel.DataAnnotations;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class PeliculaCreacionDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        [SizeImagenValidacion(pesoMaximoMB: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }
    }
}
