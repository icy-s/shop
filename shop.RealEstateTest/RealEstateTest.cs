using Microsoft.EntityFrameworkCore;
using shop.Core.Dto;
using shop.Core.ServiceInterface;

namespace shop.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new();

            dto.Area = 120.5;
            dto.Location = "Downtown";
            dto.RoomNumber = 3;
            dto.BuildingType = "Apartment";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            // Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("e83448d3-ee24-4e02-8544-f105743ccafb");

            // Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            // Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("e83448d3-ee24-4e02-8544-f105743ccafb");
            Guid guid = Guid.Parse("e83448d3-ee24-4e02-8544-f105743ccafb");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate
                = await Svc<IRealEstateServices>().Create(dto);
            var deletedRealEstate 
                = await Svc<IRealEstateServices>().Delete((Guid)createdRealEstate.Id);

            //Assert
            Assert.Equal(deletedRealEstate, createdRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate1
                = await Svc<IRealEstateServices>().Create(dto);
            var createdRealEstate2
                = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)createdRealEstate2.Id);

            //Assert
            Assert.NotEqual(result.Id, createdRealEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //Arrange
            var guid = new Guid("7f7769cf-e139-4c6b-a2f2-467785f987b8");

            RealEstateDto dto = MockRealEstateData();
            RealEstateDto domain = new();

            domain.Id = Guid.Parse("7f7769cf-e139-4c6b-a2f2-467785f987b8");
            domain.Area = 200.0;
            domain.Location = "Secret Place";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            //Act
            await Svc<IRealEstateServices>().Update(dto);

            //Assert
            Assert.Equal(guid, domain.Id);
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            Assert.NotEqual(dto.Area, domain.Area);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData2()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();

            var result = await Svc<IRealEstateServices>().Update(update);

            //Assert
            Assert.DoesNotMatch(dto.Location, result.Location);
            Assert.NotEqual(dto.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto update = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //Assert
            Assert.NotEqual(dto.Id, result.Id);
        }

        [Fact]
        // Kontrollime, et Create tõepoolest tagastab objekti täidetud Id-ga.
        public async Task Should_CreateRealEstate_WithValidData()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();

            // Act
            var created = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotEqual(Guid.Empty, created.Id);
            Assert.Equal(dto.Location, created.Location);
            Assert.Equal(dto.RoomNumber, created.RoomNumber);
        }

        [Fact]
        // Loogiline stsenaarium: loome → saame ID järgi → võrdleme välju.
        public async Task Should_ReturnSameRealEstate_WhenGetDetailsAfterCreate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();

            // Act
            var created = await Svc<IRealEstateServices>().Create(dto);
            var fetched = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched.Id);
            Assert.Equal(created.Location, fetched.Location);
        }

        [Fact]
        // Uuendame objekti, millel puudub ID – teenus peab tagastama null või muu oodatava tulemuse.
        public async Task ShouldNot_UpdateRealEstate_WhenIdDoesNotExist()
        {
            // Arrange
            RealEstateDto update = MockUpdateRealEstateData();
            update.Id = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await Svc<IRealEstateServices>().Update(update);
            });
        }


        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 150.0,
                Location = "Uptown",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return dto;
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 100.0,
                Location = "Mountain",
                RoomNumber = 3,
                BuildingType = "Cabin log",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return dto;
        }

        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Id = null,
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null,
            };

            return dto;
        }
    }
}