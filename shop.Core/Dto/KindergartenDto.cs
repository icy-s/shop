using Microsoft.AspNetCore.Http;
using shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Core.Dto
{
    public class KindergartenDto
    {
        public Guid Id { get; set; }
        public string? GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> FileToDatabaseDto { get; set; }
= new List<FileToDatabaseDto>();
    }
}
