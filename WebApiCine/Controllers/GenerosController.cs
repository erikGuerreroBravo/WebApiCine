using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiCine.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenerosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Get()
        {
            return await _context.Generos.ToListAsync();  
        }
    }
}
