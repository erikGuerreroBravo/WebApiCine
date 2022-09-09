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


        public async Task<ActionResult<List<Genero>>> Obtener()
        {
            return await _context.Generos.ToListAsync();    
        }


    }
}
