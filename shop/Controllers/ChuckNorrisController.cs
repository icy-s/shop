using Microsoft.AspNetCore.Mvc;
using shop.ApplicationServices.Services;
using System.Threading.Tasks;

namespace shop.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly ChuckNorrisServices _jokeService;

        public ChuckNorrisController(ChuckNorrisServices jokeService)
        {
            _jokeService = jokeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomJoke()
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            return Json(new { joke });
        }
    }
}