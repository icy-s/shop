using Microsoft.EntityFrameworkCore;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using shop.SpaceshipTest;

namespace shop.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        private SpaceshipDto MockSpaceshipData()
        {
            return new SpaceshipDto
            {
                Name = "Explorer",
                TypeName = "Battlecruiser",
                BuiltDate = DateTime.Now.AddYears(-3),
                Crew = 120,
                EnginePower = 8000,
                Passengers = 50,
                InnerVolume = 700,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        private SpaceshipDto MockUpdatedSpaceshipData(Guid id)
        {
            return new SpaceshipDto
            {
                Id = id,
                Name = "Explorer MK2",
                TypeName = "Heavy Cruiser",
                BuiltDate = DateTime.Now.AddYears(-1),
                Crew = 200,
                EnginePower = 12000,
                Passengers = 80,
                InnerVolume = 900,
                CreatedAt = DateTime.Now.AddYears(-3),
                ModifiedAt = DateTime.Now
            };
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_CreateSpaceship_WithValidData()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var result = await Svc<ISpaceshipsServices>().Create(dto);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Crew, result.Crew);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_ReturnSameSpaceship_WhenGetDetailsAfterCreate()
        {
            SpaceshipDto dto = MockSpaceshipData();

            var created = await Svc<ISpaceshipsServices>().Create(dto);
            var fetched = await Svc<ISpaceshipsServices>().DetailAsync((Guid)created.Id);

            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched.Id);
            Assert.Equal(created.Name, fetched.Name);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_DeleteSpaceship_WhenDeleteById()
        {
            var dto = MockSpaceshipData();
            var created = await Svc<ISpaceshipsServices>().Create(dto);
            var deleted = await Svc<ISpaceshipsServices>().Delete((Guid)created.Id);

            Assert.Equal(created.Id, deleted.Id);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateData()
        {
            var dto = MockSpaceshipData();
            var created = await Svc<ISpaceshipsServices>().Create(dto);

            Svc<ShopContext>().ChangeTracker.Clear();

            SpaceshipDto update = MockUpdatedSpaceshipData((Guid)created.Id);

            var updated = await Svc<ISpaceshipsServices>().Update(update);

            Assert.Equal(update.Id, updated.Id);
            Assert.NotEqual(created.Name, updated.Name);
            Assert.NotEqual(created.EnginePower, updated.EnginePower);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task ShouldNot_UpdateSpaceship_WhenIdDoesNotExist()
        {
            SpaceshipDto update = MockSpaceshipData();
            update.Id = Guid.NewGuid();

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await Svc<ISpaceshipsServices>().Update(update);
            });
        }
    }
}
