using Microsoft.EntityFrameworkCore;
using ReactCRUD.Core.Domain;
using ReactCRUD.Core.ServiceInterface;
using ReactCRUD.Data;

namespace ReactCRUD.ApplicationServices.Services
{
    public class SchoolServices : SchoolInterface
    {
        private readonly ReactCRUDContext _context;

        public SchoolServices(ReactCRUDContext context)
        {
            _context = context;
        }

        public async Task<School> SchoolDetail(Guid id)
        {
            var result = await _context.Schools
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<School> SchoolDelete(Guid id)
        {
            var school = await _context.Schools
                .FirstOrDefaultAsync(x => x.Id == id);

            if (school != null)
            {
                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();
            }
            return school!;
        }
    }
}
