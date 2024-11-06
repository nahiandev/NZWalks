using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> DeleteAsync(Guid id);
        Task<RegionDTO> UpdateAsync(Guid id, UpdateRegionDTO new_region);
        Task<Region> CreateAsync(AddRegionDTO incoming_region);
    }
}
