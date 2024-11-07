using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> DeleteAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region new_region);
        Task<Region> CreateAsync(Region incoming_region);

        Task<Dictionary<string, int>> CountAsync();

        Task BulkDeleteAsync();
    }
}
