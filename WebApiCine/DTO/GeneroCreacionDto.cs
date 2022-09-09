using System.ComponentModel.DataAnnotations;

namespace WebApiCine.DTO
{
    public class GeneroCreacionDto
    {

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
