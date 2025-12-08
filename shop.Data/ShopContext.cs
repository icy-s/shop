using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options)
        : base(options) { }
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToDatabase> FileToDatabase { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
    }
}