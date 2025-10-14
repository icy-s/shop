using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.ApplicationServices.Services;
using shop.Core.Domain;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using shop.Models.Kindergarten;

namespace shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopContext _context;
        private readonly IKindergartenServices _kindergartenServices;
        private readonly IFileServices _fileServices;

        public KindergartenController
            (
            
                ShopContext context,
                IKindergartenServices kindergartenServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergarten
                .Select(x => new KindergartenIndexViewModel
                {
                    id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Files = vm.Files,
                FileToDatabaseDto = vm.Images
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageTitle = x.ImageTitle,
                        ImageData = x.ImageData,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabase
            .Where(x => x.KindergartenId == id)
            .Select(y => new ImageViewModel
            {
                ImageData = y.ImageData,
                ImageId = y.Id,
            }).ToArrayAsync();

            var vm = new KindergartenDeleteViewModel();

            vm.id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kindergarten = await _kindergartenServices.Delete(id);

            if (kindergarten == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabase
            .Where(x => x.KindergartenId == id)
            .Select(y => new ImageViewModel
            {
                ImageData = y.ImageData,
                ImageId = y.Id,
            }).ToArrayAsync();

            var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Images.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                FileToDatabaseDto = vm.Images
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageTitle = x.ImageTitle,
                        ImageData = x.ImageData,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabase
            .Where(x => x.KindergartenId == id)
            .Select(y => new ImageViewModel
            {
                ImageData = y.ImageData,
                ImageId = y.Id,
            }).ToArrayAsync();

            var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            //peate läbi viewModeli edastama Id dto -sse
            //tuleb esile kustuda removeImageFromAppi meetod
            //kui image on null, siis returib Index vaatele

            // 1) Собираем dto из viewModel
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId,
                // SpaceshipId = vm.SpaceshipId,
                // ExistingFilePath = vm.FilePath
            };

            // 2) Вызываем сервис удаления
            var image = await _fileServices.RemoveImageFromApi(dto);

            // 3) Если картинка не найдена → возврат к списку
            if (image == null)
                return RedirectToAction(nameof(Index));

            // 4) Если удалена успешно → тоже возврат к списку
            return RedirectToAction(nameof(Index));
        }
    }
}
