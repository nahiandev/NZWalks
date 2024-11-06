using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Guid id);
        Task<Region?> DeleteAsync(Guid id);
    }
}
