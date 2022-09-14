using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCine.Servicios;

namespace WebApiCine.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        public PeliculasController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorArchivos)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.almacenadorArchivos = _almacenadorArchivos;
        }


    }
}
