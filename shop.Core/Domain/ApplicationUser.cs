using Microsoft.AspNetCore.Identity;

namespace shop.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public string Name { get; set; }
    }
}