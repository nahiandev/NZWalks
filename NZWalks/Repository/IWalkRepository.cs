using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filter_property = null, string? query = null, string? order_by = null, bool is_ascending = true);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk new_walk);
        Task<Walk> RemoveAsync(Guid id);
    }
}
