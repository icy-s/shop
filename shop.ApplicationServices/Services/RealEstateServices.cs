using Microsoft.EntityFrameworkCore;
using shop.Core.Domain;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;

namespace shop.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
            private readonly ShopContext _context;
            private readonly IFileServices _fileServices;

            // teha constructor
            public RealEstateServices
                (
                    ShopContext context,
                    IFileServices fileServices
                )
            {
                _context = context;
                _fileServices = fileServices;
            }
            public async Task<RealEstate> Create(RealEstateDto dto)
            {
                RealEstate realestate = new RealEstate();

                realestate.Id = Guid.NewGuid();
                realestate.Area = dto.Area;
                realestate.Location = dto.Location;
                realestate.RoomNumber = dto.RoomNumber;
                realestate.BuildingType = dto.BuildingType;
                realestate.CreatedAt = DateTime.Now;
                realestate.ModifiedAt = DateTime.Now;

                if (dto.Files != null)
                {
                    _fileServices.UploadFilesToDatabase(dto, realestate);
                }

                await _context.RealEstates.AddAsync(realestate);
                await _context.SaveChangesAsync();

                return realestate;
            }

            public async Task<RealEstate> DetailAsync(Guid id)
            {
                var result = await _context.RealEstates
                    .FirstOrDefaultAsync(x => x.Id == id);

                return result;
            }
            public async Task<RealEstate> Delete(Guid id)
            {
                var realestate = await _context.RealEstates
                    .FirstOrDefaultAsync(x => x.Id == id);

                _context.RealEstates.Remove(realestate);
                await _context.SaveChangesAsync();

                return realestate;
            }
            public async Task<RealEstate> Update(RealEstateDto dto)
            {
                RealEstate domain = new();

                domain.Id = dto.Id;
                domain.Area = dto.Area;
                domain.Location = dto.Location;
                domain.RoomNumber = dto.RoomNumber;
                domain.BuildingType = dto.BuildingType;
                domain.CreatedAt = dto.CreatedAt;
                domain.ModifiedAt = DateTime.Now;

                _context.RealEstates.Update(domain);
                await _context.SaveChangesAsync();
                _fileServices.UploadFilesToDatabase(dto, domain);

                return domain;
            }
        }
    }