using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
