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
        void FilesToApi(KindergartenDto dto, Kindergarten kindergarten);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
    }
}
