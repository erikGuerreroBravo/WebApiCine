using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

        }
    }
}
