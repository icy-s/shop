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
    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly ShopContext _context;

        public SpaceshipsServices
            (
                ShopContext context
            )
        {
            _context = context;
        }
        public async Task<Spaceship> Create(SpaceshipDto dto)
        {
            Spaceship spaceship = new Spaceship();

            spaceship.Id = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.TypeName = dto.TypeName;
            spaceship.BuiltDate = dto.BuiltDate;
            spaceship.Crew = dto.Crew;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.Passengers = dto.Passengers;
            spaceship.InnerVolume = dto.InnerVolume;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }

        public async Task<Spaceship> DetailAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Spaceship> Delete(Guid id)
        {
            var spaceship = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Spaceships.Remove(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }
    }
}
