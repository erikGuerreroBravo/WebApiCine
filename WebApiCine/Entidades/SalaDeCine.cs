﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCine.Entidades
{
    public class SalaDeCine
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
    }
}
