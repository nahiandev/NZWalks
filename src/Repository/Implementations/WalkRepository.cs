﻿using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Repository.Interfaces;

namespace NZWalks.Repository.Implementations
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksRecordsDbContext _context;

        public WalkRepository(NZWalksRecordsDbContext context) => _context = context;

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

        public async Task<List<Walk>> GetAllAsync(string? filter_property = null,
            string? query = null, string? order_by = null,
            bool is_ascending = true, int page_number = 1,
            int page_size = 1000)
        {
            List<string> filters = ["Name", "Length", "Difficulty"];
            bool valid_filter = filters.Any(filter => filter.Equals(filter_property, StringComparison.OrdinalIgnoreCase));

            if (filter_property is not null && !valid_filter) return null;
            if (valid_filter && query is null) return null;
            if (filter_property is null && query is not null) return null;

            IQueryable<Walk> walks = _context.Walks.Include(d => d.Difficulty)
                .Include(r => r.Region).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter_property) && !string.IsNullOrWhiteSpace(query))
            {
                if (filter_property.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(query));
                }

                else if (filter_property.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    bool is_number = double.TryParse(query.Trim(), out double result);

                    if (is_number)
                    {
                        walks = walks.Where(w => w.LengthInKM <= result);
                    }
                    return null;
                }

                else if (filter_property.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Difficulty.Name.Contains(query));

                }
            }

            //Sorting
            int count = walks.Count();
            if (order_by is not null && count > 0)
            {
                if (order_by.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = is_ascending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }

                else if (order_by.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = is_ascending ? walks.OrderBy(w => w.LengthInKM) : walks.OrderByDescending(w => w.LengthInKM);
                }
            }

            // Pagination
            int skip_results = (page_number - 1) * page_size;

            return await walks.Skip(skip_results).Take(page_size).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await _context.Walks
                .Include(d => d.Difficulty)
                .Include(r => r.Region)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (walk is null) return null;

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk new_walk)
        {
            var walk = await _context.Walks.FindAsync(id);

            if (walk is null) return null;


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
