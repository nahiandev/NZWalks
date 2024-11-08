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

            //Walk? match = (await walks.ToListAsync()).Find(__ => __.Id == walk.Id);


            var exists = await walks.AnyAsync(_ => _.Id == walk.Id);

            if (exists) return null;
            
            await walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }
    }
}
