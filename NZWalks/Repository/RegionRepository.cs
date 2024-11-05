using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var all_items = await _context.Regions.ToListAsync();
            return all_items;
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            var item = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }
    }
}
