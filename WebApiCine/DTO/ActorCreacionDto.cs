using System.ComponentModel.DataAnnotations;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class ActorCreacionDto : ActorPatchDto
    {
       
        [SizeImagenValidacion(pesoMaximoMB:4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
        
    }
}
