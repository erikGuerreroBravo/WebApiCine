using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCine.DTO;
using WebApiCine.Entidades;

namespace WebApiCine.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDto>>> Get()
        {
            var entidades = await _context.Generos.ToListAsync();
            var generos= _mapper.Map<List<GeneroDto>>(entidades);
            return generos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Obtener()
        {
            return await _context.Generos.ToListAsync();    
        }
        [HttpGet("{id:int}", Name="obtenerGenero")]
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            var entidad = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<GeneroDto>(entidad);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDto creacionDto)
        {
            var entidad  =_mapper.Map<GeneroDto>(creacionDto);
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var generoDTO = _mapper.Map<GeneroDto>(entidad);
            return new CreatedAtRouteResult("obtenerGenero",new { id=generoDTO.Id},generoDTO);
        }



    }
}
