using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCine.DTO;
using WebApiCine.Entidades;

namespace WebApiCine.Controllers
{
    [Route("api/SalasDeCine")]
    [ApiController]
    public class SalasDeCineController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public SalasDeCineController(ApplicationDbContext _context, IMapper _mapper) : base(_context, _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDto>>> Get()
        {
            return await Get<SalaDeCine, SalaDeCineDto>();
        }
        [HttpGet("{id:int}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDto>> Get(int id)
        {
            return await Get<SalaDeCine, SalaDeCineDto>(id);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            return await Post<SalaDeCineCreacionDto, SalaDeCine, SalaDeCineDto>(salaDeCineCreacionDto, "obtenerSalaDeCine");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            return await Put<SalaDeCineCreacionDto, SalaDeCine>(id, salaDeCineCreacionDto);
        }
    }
}
