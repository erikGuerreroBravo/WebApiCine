using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class 
        {

        }


    }
}
