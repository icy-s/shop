using shop.Core.Domain;
using shop.Core.Dto;

namespace shop.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> DetailAsync(Guid id);
        Task<RealEstate> Delete(Guid id);
        Task<RealEstate> Update(RealEstateDto dto);
    }
}