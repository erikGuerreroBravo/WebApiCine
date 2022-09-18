using System.ComponentModel.DataAnnotations;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class PeliculaPatchDto
    {
        
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
    }
}
