using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
using WebApiCine.Entidades;

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
        public async Task<ActionResult<List<ActorDto>>> Get() {
            var actores = await this.context.Actores.ToListAsync();
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
        public async Task<ActionResult> Post([FromBody] ActorCreacionDto actorCreacionDto) 
        {
            var  actor = mapper.Map<Actor>(actorCreacionDto);
            context.Add(actor);
            await context.SaveChangesAsync();
            var actores = mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("obtenerActor", new {id= actor.Id }, actores);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreacionDto actorCreacionDto)
        {
            var actor = mapper.Map<Actor>(actorCreacionDto);
            actor.Id = id;
            context.Entry(actor).State = EntityState.Modified;
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
            context.Remove(new Genero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }




    }
}
