using Microsoft.AspNetCore.Mvc;
using ReactCRUD.Data;

namespace ReactCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ReactCRUDContext _context;

        public SchoolController
            (
                ReactCRUDContext context
            )
        {

        }

        [HttpGet(Name = "SchoolList")]
        public IActionResult Index()
        {
            var result = "";

            return Ok(result);
        }
    }
}
