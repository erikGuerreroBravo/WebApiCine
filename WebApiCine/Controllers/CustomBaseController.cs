using Microsoft.AspNetCore.Mvc;

namespace WebApiCine.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CustomBaseController(ApplicationDbContext context)
        {
            this.context = context; 
        }
    }
}
