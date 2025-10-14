using shop.Core.Domain;
using shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
    }
}
