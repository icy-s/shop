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
            Guid databaseGuid = Guid.Parse("e83448d3-ee24-4e02-8544-f105743ccafb");
            Guid guid = Guid.Parse("e83448d3-ee24-4e02-8544-f105743ccafb");
        }
    }
}
