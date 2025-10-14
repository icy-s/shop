using Microsoft.EntityFrameworkCore;
using shop.Core.Domain;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopContext _context;
        private readonly IFileServices _fileServices;
        public KindergartenServices
    (
        ShopContext context,
        IFileServices fileServices
    )
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.CreatedAt = dto.CreatedAt;
            kindergarten.UpdatedAt = dto.UpdatedAt;

            _fileServices.FilesToApi(dto, kindergarten);

            await _context.Kindergarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }
        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToApis
            .Where(x => x.KindergartenId == id)
            .Select(y => new FileToApiDto
            {
                Id = y.Id,
                KindergartenId = y.KindergartenId,
                ExistingFilePath = y.ExistingFilePath,
            }).ToArrayAsync();

            await _fileServices.RemoveImageFromDatabase(images);

            _context.Kindergarten.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }
        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten domain = new();

            domain.Id = dto.Id;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergartenName = dto.KindergartenName;
            domain.TeacherName = dto.TeacherName;
            domain.CreatedAt = dto.CreatedAt;
            domain.UpdatedAt = DateTime.Now;

            _fileServices.FilesToApi(dto, domain);

            _context.Kindergarten.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
