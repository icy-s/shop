using Microsoft.AspNetCore.Mvc;
using ReactCRUD.Core.ServiceInterface;
using ReactCRUD.Data;
using ReactCRUD.Server.ViewModels;

namespace ReactCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ReactCRUDContext _context;
        private readonly SchoolInterface _schoolService;

        public SchoolController
            (
                ReactCRUDContext context,
                SchoolInterface schoolService
            )
        {
            _context = context;
            _schoolService = schoolService;
        }

        [HttpGet(Name = "SchoolList")]
        public IActionResult Index()
        {
            var result = _context.Schools
                .Select(x => new SchoolListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    StudentCount = x.StudentCount,
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "SchoolDetail")]
        public IActionResult Detail(Guid id)
        {
            var school = _schoolService.SchoolDetail(id);
            if (school == null)
            {
                return NotFound();
            }

            var result = new SchoolDetailViewModel
                {
                    Id = school.Result.Id,
                    Name = school.Result.Name,
                    Address = school.Result.Address,
                    StudentCount = school.Result.StudentCount,
                };

            return Ok(result);
        }
    }
}
