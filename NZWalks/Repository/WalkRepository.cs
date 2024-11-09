using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;

        public WalkRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            var walks = _context.Walks;

            var exists = await walks.AnyAsync(_ => _.Id == walk.Id);

            if (exists) return null;
            
            await walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> RemoveAsync(Guid id)
        {
            var selected_to_delete = await _context.Walks.FindAsync(id);

            if (selected_to_delete is null) return null;
            
                _context.Walks.Remove(selected_to_delete);
                await _context.SaveChangesAsync();
            
            return selected_to_delete;
        }

        public async Task<List<Walk>> GetAllAsync(string? filter_property = null, string? query = null)
        {
            //var walks = await _context.Walks.Include(d => d.Difficulty).Include(r => r.Region)
            //    .ToListAsync();
            //return walks;


            var walks = _context.Walks.Include(d => d.Difficulty)
                .Include(r => r.Region).AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(filter_property) && !String.IsNullOrWhiteSpace(query))
            {
                if (filter_property.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(query));
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await _context.Walks
                .Include(d => d.Difficulty)
                .Include(r => r.Region)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (walk is null) return null;
            
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk new_walk)
        {
            var walk = await _context.Walks.FindAsync(id);

            if (walk is null)
            {
                return null;
            }

            walk.Name = new_walk.Name;
            walk.Description = new_walk.Description;
            walk.LengthInKM = new_walk.LengthInKM;
            walk.WalkImageUrl = new_walk.WalkImageUrl;
            walk.DifficultyId = new_walk.DifficultyId;
            walk.RegionId = new_walk.RegionId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }
    }
}
