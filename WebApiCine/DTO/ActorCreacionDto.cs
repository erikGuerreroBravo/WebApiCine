using System.ComponentModel.DataAnnotations;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class ActorCreacionDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [SizeImagenValidacion(pesoMaximoMB:4)]
        [TipoArchivoValidacion(tiposValidos:new string[] { "image/jpeg","image/png","image/gif"})]
        public IFormFile Foto { get; set; }
        
    }
}
