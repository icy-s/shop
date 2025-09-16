using shop.Core.Domain;
using shop.Core.Dto;

namespace shop.Core.ServiceInterface
{
    public interface  IKindergartenServices
    {
        Task<Kindergarten> Create(KindergartenDto dto);
        Task<Kindergarten> DetailAsync(Guid id);
        Task<Kindergarten> Delete(Guid id);
        Task<Kindergarten> Update(KindergartenDto dto);
    }
}
