using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var visitor_role_id = "cd3d647c-c1e6-4812-afd8-7b250e2857b9";
            var admin_role_id = "d01ae10e-9694-499e-afe4-839af3f8f00e";

            List<IdentityRole> roles = [
                new()
                {
                    Id = admin_role_id,
                    ConcurrencyStamp = admin_role_id,
                    Name = "Admin",
                    NormalizedName = "Admin".ToLower()
                },
                new()
                {
                    Id = visitor_role_id,
                    ConcurrencyStamp = visitor_role_id,
                    Name = "Visitor",
                    NormalizedName = "Visitor".ToLower()
                }
            ];

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
