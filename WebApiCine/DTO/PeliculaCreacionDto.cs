using System.ComponentModel.DataAnnotations;

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

        public IFormFile Poster { get; set; }
    }
}
