using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
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
            
        }

    }
}
