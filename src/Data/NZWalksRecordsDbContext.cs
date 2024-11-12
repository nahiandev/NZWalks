using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksRecordsDbContext : DbContext
    {
        public NZWalksRecordsDbContext(DbContextOptions<NZWalksRecordsDbContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }


        //protected override void OnModelCreating(ModelBuilder model_builder)
        //{
        //    base.OnModelCreating(model_builder);

        //    var regions = new List<Region>
        //    {
        //        new()
        //        {
        //            Id = Guid.Parse("d043934d-3481-48df-860a-e8199e9aa923"),
        //            Code = "FL",
        //            Name = "Florida",
        //            RegionImageUrl = "https://dummyimage.com/400x400/000/fff.png"
        //        },
        //        new()
        //        {
        //            Id = Guid.Parse("b123f4de-5678-90ab-cdef-111213141516"),
        //            Code = "CAF",
        //            Name = "California",
        //            RegionImageUrl = "https://dummyimage.com/400x400/ff0000/fff.png"
        //        },
        //        new()
        //        {
        //            Id = Guid.Parse("c67890ab-1234-56de-7890-123456789012"),
        //            Code = "NYK",
        //            Name = "New York",
        //            RegionImageUrl = "https://dummyimage.com/400x400/00ff00/fff.png"
        //        },
        //        new()
        //        {
        //            Id = Guid.Parse("d7890123-4567-89ab-cdef-098765432123"),
        //            Code = "TXS",
        //            Name = "Texas",
        //            RegionImageUrl = "https://dummyimage.com/400x400/0000ff/fff.png"
        //        },
        //        new()
        //        {
        //            Id = Guid.Parse("e0123456-7890-12ab-cdef-123456789abc"),
        //            Code = "WAS",
        //            Name = "Washington",
        //            RegionImageUrl = "https://dummyimage.com/400x400/ffff00/000.png"
        //        },
        //        new()
        //        {
        //            Id = Guid.Parse("73b191f8-fe5f-4a0e-b7d3-c04a886f1c46"),
        //            Code = "LAS",
        //            Name = "Washington",
        //            RegionImageUrl = "https://dummyimage.com/400x400/ffff00/000.png"
        //        }
        //    };

        //    model_builder.Entity<Region>().HasData(regions);

        //    //List<Difficulty> difficulties =
        //    //[
        //    //    new()
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "Easy"
        //    //    },
        //    //    new()
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "Medium"
        //    //    },
        //    //    new()
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "Hard"
        //    //    }
        //    //];

        //    //model_builder.Entity<Difficulty>().HasData(difficulties);


            
        //}
    }
}
