using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksRecordsDbContext _context;

        public RegionRepository(NZWalksRecordsDbContext context)
        {
            _context = context;
        }

        public async Task BulkDeleteAsync()
        {
            var context = _context.Regions;
            var regions = await context.ToListAsync();

            if (regions.Count > 0)
            {
                foreach (var region in regions)
                {
                    context.Remove(region);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<string, int>> CountAsync()
        {
            var count = await _context.Regions.CountAsync();
            return new Dictionary<string, int> { { "count", count } };
        }

        public async Task<Region> CreateAsync(Region incoming_region)
        {
            if (incoming_region != null)
            {
                await _context.Regions.AddAsync(incoming_region);
                await _context.SaveChangesAsync();
            }

            return incoming_region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var selected_item = await _context.Regions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (selected_item is null) return null;

            _context.Regions.Remove(selected_item);
            await _context.SaveChangesAsync();

            return selected_item;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var all_items = await _context.Regions.ToListAsync();
            return all_items;
        }

        /*  
            Repository pattern to separate the 
            data access logic from the business logic.
        */
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var item = await _context.Regions
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region new_region)
        {
            var existing_item = await _context.Regions
                .FirstOrDefaultAsync(_ => _.Id == id);

            if (existing_item is null) return null;

            existing_item.Code = new_region.Code;
            existing_item.Name = new_region.Name;
            existing_item.RegionImageUrl = new_region.RegionImageUrl;
            await _context.SaveChangesAsync();

            return existing_item;
        }
    }
}
