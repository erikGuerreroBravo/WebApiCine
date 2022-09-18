using AutoMapper;
/**cargamos esta libreria desde nugget para trabajar con JsonPatch***/
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
using WebApiCine.Entidades;
using WebApiCine.Helpers;

namespace WebApiCine.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext _context, IMapper _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> Get([FromQuery] PaginacionDto paginacionDto) {
            /*mandamos traer la consulta de todos los actores*/
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDto.CantidadRegistrosPorPagina);
            var actores = await this.context.Actores.Paginar(paginacionDto).ToListAsync();
            return mapper.Map<List<ActorDto>>(actores);
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            var entidad = await this.context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<ActorDto>(entidad);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDto actorCreacionDto) 
        {
            var  actor = mapper.Map<Actor>(actorCreacionDto);
            context.Add(actor);
            await context.SaveChangesAsync();
            var actores = mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("obtenerActor", new {id= actor.Id }, actores);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDto actorCreacionDto)
        {
            var actor = mapper.Map<Actor>(actorCreacionDto);
            actor.Id = id;
            context.Entry(actor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.Actores.FirstOrDefaultAsync(x=> x.Id == id);
            if (entidadDB == null) 
            {
                return NotFound();
            }
            var entidadDto = mapper.Map<ActorPatchDto>(entidadDB);

            ///aplicamos el patch de la siguiente manera
            ///pero necesitamos instalar Microsoft.AspNetCore.Mvc.NewtonsoftJson
            patchDocument.ApplyTo(entidadDto, ModelState);
            ///validamos que se complan con todos los requisitos y validaciones del modelo.
            var esValido = TryValidateModel(entidadDto);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(entidadDto, entidadDB);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Actores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Actor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }




    }
}
