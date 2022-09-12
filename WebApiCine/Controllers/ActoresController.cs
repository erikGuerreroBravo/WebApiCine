using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;

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
            return  mapper.Map<List<ActorDto>>(actores);
        }
    }
}
