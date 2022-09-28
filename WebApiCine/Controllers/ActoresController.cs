using AutoMapper;
/**cargamos esta libreria desde nugget para trabajar con JsonPatch***/
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
using WebApiCine.Entidades;
using WebApiCine.Helpers;
using WebApiCine.Servicios;

namespace WebApiCine.Controllers
{
    /// <summary>
    /// Controlador del tipo actores controller
    /// </summary>
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";
        public ActoresController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorArchivos):base(_context, _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.almacenadorArchivos = _almacenadorArchivos;
        }
        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> Get([FromQuery] PaginacionDto paginacionDto) {
            /*mandamos traer la consulta de todos los actores*/
            //var queryable = context.Actores.AsQueryable();
            //await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDto.CantidadRegistrosPorPagina);
            //var actores = await this.context.Actores.Paginar(paginacionDto).ToListAsync();
            //return mapper.Map<List<ActorDto>>(actores);
            return await Get<Actor, ActorDto>(paginacionDto);
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            //var entidad = await this.context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            //if (entidad == null)
            //{
            //    return NotFound();
            //}
            //return this.mapper.Map<ActorDto>(entidad);
            return await Get<Actor, ActorDto>(id);
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
            var actorDB =  await context.Actores.FirstOrDefaultAsync(x=>x.Id ==id);
            if(actorDB == null) { return NotFound(); }
            actorDB = mapper.Map(actorCreacionDto,actorDB);
            if (actorCreacionDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDto.Foto.FileName);
                    actorDB.Foto = await almacenadorArchivos.EditarArchivo(contenido,extension,contenedor, actorDB.Foto, 
                        actorCreacionDto.Foto.ContentType);
                }
            }
            await context.SaveChangesAsync();
            return NoContent();

            //var actor = mapper.Map<Actor>(actorCreacionDto);
            //actor.Id = id;
            //context.Entry(actor).State = EntityState.Modified;
            //await context.SaveChangesAsync();
            //return NoContent();

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            //if (patchDocument == null)
            //{
            //    return BadRequest();
            //}
            //var entidadDB = await context.Actores.FirstOrDefaultAsync(x=> x.Id == id);
            //if (entidadDB == null) 
            //{
            //    return NotFound();
            //}
            //var entidadDto = mapper.Map<ActorPatchDto>(entidadDB);

            ///aplicamos el patch de la siguiente manera
            ///pero necesitamos instalar Microsoft.AspNetCore.Mvc.NewtonsoftJson
            //patchDocument.ApplyTo(entidadDto, ModelState);
            /////validamos que se complan con todos los requisitos y validaciones del modelo.
            //var esValido = TryValidateModel(entidadDto);
            //if (!esValido)
            //{
            //    return BadRequest(ModelState);
            //}
            //mapper.Map(entidadDto, entidadDB);
            //await context.SaveChangesAsync();
            //return NoContent();
            return await Patch<Actor, ActorPatchDto>(id,patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var existe = await context.Actores.AnyAsync(x => x.Id == id);
            //if (!existe)
            //{
            //    return NotFound();
            //}
            //context.Remove(new Actor() { Id = id });
            //await context.SaveChangesAsync();
            //return NoContent();
            return await Delete<Actor>(id);
        }




    }
}
