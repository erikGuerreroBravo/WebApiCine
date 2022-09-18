﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
using WebApiCine.Entidades;
using WebApiCine.Servicios;

namespace WebApiCine.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        //creacion del contenedor o carpeta de archivos donde se va a almacenar las fotos
        private readonly string contenedor = "peliculas";
        public PeliculasController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorArchivos)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.almacenadorArchivos = _almacenadorArchivos;
        }
        [HttpGet]
        public async Task<ActionResult<List<PeliculaDto>>> Get()
        {
            var peliculas = await context.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDto>>(peliculas);
        }
        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (pelicula == null)
            {
                return NotFound();  
            }
            return mapper.Map<PeliculaDto>(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDto peliculaCreacionDto)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDto);
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaCreacionDto.Poster.ContentType);
                }
            }
        }

    }
}
