using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.ApplicationServices.Services;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using shop.Models.Spaceships;

namespace shop.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstateServices _realestateServices;
        private readonly IFileServices _fileServices;


        public RealEstateController
            (
                ShopContext context,
                IRealEstateServices realestateServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _realestateServices = realestateServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BuiltDate = x.BuiltDate,
                    TypeName = x.TypeName,
                    Crew = x.Crew,
                });


            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _realestateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realestate = await _realestateServices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id,
            vm.Area = realestate.Area,
            vm.Location = realestate.Location,
            vm.RoomNumber = realestate.RoomNumber,
            vm.BuildingType = realestate.BuildingType,
            vm.CreatedAt = realestate.CreatedAt,
            vm.ModifiedAt = realestate.ModifiedAt,

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {

            var deleted = await _realestateServices.Delete(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var update = await _realestateServices.DetailAsync(id);

            if (update == null)
            {
                return NotFound();
            }

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = update.Id,
            vm.Area = update.Area,
            vm.Location = update.Location,
            vm.RoomNumber = update.RoomNumber,
            vm.BuildingType = update.BuildingType,
            vm.CreatedAt = update.CreatedAt,
            vm.ModifiedAt = update.ModifiedAt,

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _realestateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _realestateervices.DetailAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id,
            vm.Area = realestate.Area,
            vm.Location = realestate.Location,
            vm.RoomNumber = realestate.RoomNumber,
            vm.BuildingType = realestate.BuildingType,
            vm.CreatedAt = realestate.CreatedAt,
            vm.ModifiedAt = realestate.ModifiedAt,

            return View(vm);
        }
    }
}