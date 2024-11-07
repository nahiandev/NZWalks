using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder model_builder)
        {
            base.OnModelCreating(model_builder);

            var difficulties = new List<Difficulty>
            {
                new()
                {
                    Id = Guid.Parse("d043934d-3481-48df-860a-e8199e9aa923"),
                    Name = "Easy"
                },
                new()
                {
                    Id = Guid.Parse("6c1366bd-7194-4450-b22b-63ad8420d137"),
                    Name = "Medium"
                },
                new()
                {
                    Id = Guid.Parse("1c900f7f-82be-4ea7-8908-1a748fc5bcae"),
                    Name = "Hard"
                }
            };

            model_builder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {
                new()
                {
                    Id = Guid.Parse("d043934d-3481-48df-860a-e8199e9aa923"),
                    Code = "FL",
        Name = "Florida",
        RegionImageUrl = "https://dummyimage.com/400x400/000/fff.png"
    },
    new()
    {
        Id = Guid.Parse("b123f4de-5678-90ab-cdef-111213141516"),
        Code = "CA",
        Name = "California",
        RegionImageUrl = "https://dummyimage.com/400x400/ff0000/fff.png"
    },
    new()
    {
        Id = Guid.Parse("c67890ab-1234-56de-7890-123456789012"),
        Code = "NY",
        Name = "New York",
        RegionImageUrl = "https://dummyimage.com/400x400/00ff00/fff.png"
    },
    new()
    {
        Id = Guid.Parse("d7890123-4567-89ab-cdef-098765432123"),
        Code = "TX",
        Name = "Texas",
        RegionImageUrl = "https://dummyimage.com/400x400/0000ff/fff.png"
    },
    new()
    {
        Id = Guid.Parse("e0123456-7890-12ab-cdef-123456789abc"),
        Code = "WA",
        Name = "Washington",
        RegionImageUrl = "https://dummyimage.com/400x400/ffff00/000.png"
    }
};

            model_builder.Entity<Region>().HasData(regions);
        }
    }
}
