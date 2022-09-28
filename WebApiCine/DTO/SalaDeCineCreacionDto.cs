using System.ComponentModel.DataAnnotations;

namespace WebApiCine.DTO
{
    public class SalaDeCineCreacionDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
    }
}
