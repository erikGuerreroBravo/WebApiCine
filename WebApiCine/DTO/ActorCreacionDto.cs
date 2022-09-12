using System.ComponentModel.DataAnnotations;

namespace WebApiCine.DTO
{
    public class ActorCreacionDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        
    }
}
