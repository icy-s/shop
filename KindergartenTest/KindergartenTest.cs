using Microsoft.EntityFrameworkCore;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Data;
using shop.SpaceshipTest;

namespace shop.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        private KindergartenDto MockDto()
        {
            return new KindergartenDto
            {
                GroupName = "Sunflowers",
                ChildrenCount = 18,
                KindergartenName = "Bright Kids Academy",
                TeacherName = "Anna Petrova",
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-5),
                Files = null
            };
        }

        private KindergartenDto MockUpdatedDto(Guid id)
        {
            return new KindergartenDto
            {
                Id = id,
                GroupName = "Sunflowers – Updated",
                ChildrenCount = 20,
                KindergartenName = "Happy Garden",
                TeacherName = "Maria Ivanova",
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now
            };
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_CreateKindergarten_WithDefaultDates()
        {
            // Test kontrollib, et luuakse edukalt objekt,
            // isegi kui kuupäevad on MinValue.
            var dto = MockDto();
            dto.CreatedAt = DateTime.MinValue;
            dto.UpdatedAt = DateTime.MinValue;

            var result = await Svc<IKindergartenServices>().Create(dto);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(DateTime.MinValue, result.CreatedAt);
            Assert.Equal(DateTime.MinValue, result.UpdatedAt);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_ReturnNull_WhenGettingDetailsForNonExistingId()
        {
            // Test kontrollib, et DetailAsync tagastab null,
            // kui antud ID-ga lasteaeda ei eksisteeri.
            var id = Guid.NewGuid();

            var result = await Svc<IKindergartenServices>().DetailAsync(id);

            Assert.Null(result);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_UpdateTeacherName_WithoutChangingGroupName()
        {
            // Test kontrollib, et vaid õpetaja nimi muutub,
            // kuid grupi nimi jääb samaks.
            var dto = MockDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            Svc<ShopContext>().ChangeTracker.Clear();

            var updateDto = MockUpdatedDto((Guid)created.Id);
            updateDto.GroupName = created.GroupName;   // Grupp jääb samaks

            var updated = await Svc<IKindergartenServices>().Update(updateDto);

            Assert.Equal(created.GroupName, updated.GroupName);
            Assert.NotEqual(created.TeacherName, updated.TeacherName);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_DeleteKindergarten_AndRemoveImages()
        {
            // Test kontrollib, et lasteaed kustutatakse
            // ning seotud pildid eemaldatakse andmebaasist.
            var dto = MockDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var deleted = await Svc<IKindergartenServices>().Delete((Guid)created.Id);

            Assert.Equal(created.Id, deleted.Id);

            var files = Svc<ShopContext>().FileToDatabase
                .Where(x => x.KindergartenId == created.Id);

            Assert.Empty(files);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_AllowMultipleUpdates_ToSameEntity()
        {
            // Test kontrollib, et sama objekti saab mitu korda uuendada
            // ning viimane uuendus salvestub korrektselt.
            var dto = MockDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            Svc<ShopContext>().ChangeTracker.Clear();

            var first = MockUpdatedDto((Guid)created.Id);
            first.TeacherName = "Step 1";
            var result1 = await Svc<IKindergartenServices>().Update(first);

            Svc<ShopContext>().ChangeTracker.Clear();

            var second = MockUpdatedDto((Guid)created.Id);
            second.TeacherName = "Step 2";
            var result2 = await Svc<IKindergartenServices>().Update(second);

            Assert.Equal("Step 2", result2.TeacherName);
        }

        // ----------------------------------------------------------------------
        [Fact]
        public async Task Should_AllowCreatingKindergarten_EvenWithNegativeChildrenCount()
        {
            // Test kontrollib, et negatiivne laste arv ei põhjusta erindit,
            // kuna teenus ei valideeri sisendit.
            var dto = MockDto();
            dto.ChildrenCount = -10;

            var result = await Svc<IKindergartenServices>().Create(dto);

            Assert.NotNull(result);
            Assert.Equal(-10, result.ChildrenCount);
        }
    }
}
