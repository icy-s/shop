using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using shop.Models.RealEstate;

namespace shop.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstateServices _realestateServices;
        private readonly IFileServices _fileServices;

        public async Task<RealEstateImageViewModel[]> ImageShowcase(Guid id)
        {
            var photos = await _context.FileToDatabase
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealEstateImageViewModel
                {
                    RealEstateId = y.RealEstateId,
                    Id = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            return photos;
        }

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
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType,
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
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.Id,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId,
                    }).ToArray()
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
                return View("NotFound");
            }

            var photos = await ImageShowcase(id);

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {

            var deleted = await _realestateServices.Delete(id);

            if (deleted == null)
            {
                return View("NotFound");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var update = await _realestateServices.DetailAsync(id);

            if (update == null)
            {
                return View("NotFound");
            }

            var photos = await ImageShowcase(id);

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = update.Id;
            vm.Area = update.Area;
            vm.Location = update.Location;
            vm.RoomNumber = update.RoomNumber;
            vm.BuildingType = update.BuildingType;
            vm.CreatedAt = update.CreatedAt;
            vm.ModifiedAt = update.ModifiedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }

            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.Id,
                        ImageTitle = x.ImageTitle,
                        ImageData = x.ImageData,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
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
            var realestate = await _realestateServices.DetailAsync(id);

            if (realestate == null)
            {
                return View("NotFound", id);
            }

            var photos = await ImageShowcase(id);

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Location = realestate.Location;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
    }
}