using Microsoft.AspNetCore.Mvc;
using shop.Core.ServiceInterface;
using System.Threading.Tasks;

namespace shop.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _service;
        public CocktailsController(ICocktailService service) => _service = service;

        // /Cocktails?q=margarita
        [HttpGet]
        public async Task<IActionResult> Index(string q)
        {
            ViewBag.Query = q ?? "";
            var items = string.IsNullOrWhiteSpace(q)
                ? new System.Collections.Generic.List<Core.ServiceInterface.CocktailDto>()
                : await _service.SearchAsync(q);
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}