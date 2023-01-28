using BizLand.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BizLand.DAL
{
    public class BizLandContext : IdentityDbContext
    {
        public BizLandContext(DbContextOptions<BizLandContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
