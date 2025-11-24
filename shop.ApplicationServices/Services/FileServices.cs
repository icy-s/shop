using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using shop.Core.Domain;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using System.Runtime.InteropServices;
using RealEstate = shop.Core.Domain.RealEstate;

namespace shop.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopContext _context;
        private readonly IHostEnvironment _webHost;

        public FileServices
            (
                ShopContext context,
                IHostEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = spaceship.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            //meil on vaja leida file andmebaasist läbi id ülesse
            var imageId = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
                + imageId.ExistingFilePath;

            //kui fail on olemas, siis kustuta ära
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FileToApis.Remove(imageId);
            await _context.SaveChangesAsync();

            return imageId;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            //foreach, mille sees toimub failide kustutamine
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
                    + imageId.ExistingFilePath;

                //kui fail on olemas, siis kustuta ära
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.FileToApis.Remove(imageId);
                await _context.SaveChangesAsync();
            }

            return null;
        }
        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            // ära kontrollimine, kas on üks fail või mitu
            if (dto.Files != null && dto.Files.Count > 0)
            {
                // kui tuleb mitu faili, kasutatakse foreach
                foreach (var file in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            RealEstateId = domain.Id
                        };
                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabase.Add(files);
                    }
                }
            }
        }

        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToDatabase
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                _context.FileToDatabase.Remove(imageId);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            {
                var imageId = await _context.FileToDatabase
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.FileToDatabase.Remove(imageId);
                await _context.SaveChangesAsync();

                return imageId;
            }
        }
    }
}