using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.Entidades;

namespace WebApiCine.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper _mapper)
        {
            this.context = context;
            mapper = _mapper;
        }
        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class 
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync();
            var datos = mapper.Map<List<TDTO>>(entidades);
            return datos;
        
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class ,IId
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);
            
            if (entidad == null)
            {
                return NotFound();
            }
            return mapper.Map<TDTO>(entidad);

        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>(TCreacion creacionDto, string nombreRuta) 
            where TEntidad : class, IId 
        {
            var entidad = mapper.Map<TEntidad>(creacionDto);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var dtoLectura = mapper.Map<TLectura>(entidad);
            return new CreatedAtRouteResult(nombreRuta,new { id=entidad.Id}, dtoLectura);
        }

        protected async Task<ActionResult> Put<TCreacion, TEntidad>(int id, TCreacion creacionDto) where TEntidad : class, IId
        {
            var entidad = mapper.Map<Genero>(creacionDto);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
