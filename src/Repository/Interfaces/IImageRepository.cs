using NZWalks.Models.Domain;

namespace NZWalks.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
