using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

        
    }
}
