﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCine.DTO
{
    public class ActorDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
    }
}
