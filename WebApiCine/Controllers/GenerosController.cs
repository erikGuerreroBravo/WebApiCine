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
    public class GenerosController: CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GenerosController(ApplicationDbContext context, IMapper mapper): base(context,mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDto>>> Get()
        {

            return await Get<Genero, GeneroDto>();
            //var entidades = await _context.Generos.ToListAsync();
            //var generos= _mapper.Map<List<GeneroDto>>(entidades);
            //return generos;

        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Obtener()
        {
            return await _context.Generos.ToListAsync();    
        }
        [HttpGet("{id:int}", Name="obtenerGenero")]
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            return await Get<Genero, GeneroDto>(id);
            //var entidad = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            //if (entidad == null)
            //{
            //    return NotFound();
            //}
            //var dto = _mapper.Map<GeneroDto>(entidad);
            //return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDto creacionDto)
        {
            return await Post<GeneroCreacionDto, Genero, GeneroDto>(creacionDto, "obtenerGenero");
            //var entidad  =_mapper.Map<GeneroDto>(creacionDto);
            //_context.Add(entidad);
            //await _context.SaveChangesAsync();
            //var generoDTO = _mapper.Map<GeneroDto>(entidad);
            //return new CreatedAtRouteResult("obtenerGenero",new { id=generoDTO.Id},generoDTO);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDto creacionDto) 
        {
            return await Put<GeneroCreacionDto, Genero>(id, creacionDto);
            //var entidad = _mapper.Map<Genero>(creacionDto);
            //entidad.Id = id;
            //_context.Entry(entidad).State= EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Genero>(id);
            //var existe = await _context.Generos.AnyAsync(x => x.Id == id);
            //if (!existe) {
            //    return NotFound();
            //}
            //_context.Remove(new Genero() { Id = id });
            //await _context.SaveChangesAsync();
            //return NoContent();

        }

    }
}
