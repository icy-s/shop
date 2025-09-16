using shop.Core.Domain;
using shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(KindergartenDto dto, Kindergarten kindergarten);
    }
}
