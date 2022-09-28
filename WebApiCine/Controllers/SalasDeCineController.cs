using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCine.DTO;
using WebApiCine.Entidades;

namespace WebApiCine.Controllers
{
    [Route("api/SalasDeCine")]
    [ApiController]
    public class SalasDeCineController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public SalasDeCineController(ApplicationDbContext _context, IMapper _mapper): base(_context,_mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDto>>> Get() 
        {
            return await Get<SalaDeCine, SalaDeCineDto>();
        }

    }
}
