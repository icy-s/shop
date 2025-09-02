using Microsoft.AspNetCore.Mvc;
using shop.Data;
using shop.Models.Spaceships;

namespace shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopContext _context;

        public SpaceshipsController
            (
                ShopContext context
            )
        {
            _context = context;
        }


        public IActionResult Index()
        {

            var result = _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BuiltDate = x.BuiltDate,
                    TypeName = x.TypeName,
                    Crew = x.Crew
                });

            return View(result);
        }
    }
}
