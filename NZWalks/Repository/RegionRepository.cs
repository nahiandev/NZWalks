using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            _context = context;
        }


        public async Task<Region?> DeleteAsync(Guid id)
        {
            var selected_item = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(selected_item is null) return null;

            _context.Regions.Remove(selected_item);
            await _context.SaveChangesAsync();

            return selected_item;

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

        public async Task<RegionDTO> UpdateAsync(Guid id, UpdateRegionDTO new_region)
        {
            var existing_item = await _context.Regions.FirstOrDefaultAsync(_ => _.Id == id);

            if (existing_item is null) return null;

            existing_item.Code = new_region.Code;
            existing_item.Name = new_region.Name;
            existing_item.RegionImageUrl = new_region.RegionImageUrl;

            await _context.SaveChangesAsync();

            
            var updated = new RegionDTO { Code = existing_item.Code, Name = existing_item.Name, RegionImageUrl = existing_item.RegionImageUrl};


            return updated;
        }
    }
}
