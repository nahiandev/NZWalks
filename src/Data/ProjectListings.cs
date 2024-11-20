using NZWalks.Models.Domain;

namespace NZWalks.Data;

public sealed class ProjectListings
{
    public readonly static List<Project> Data = [
    new() { Id = Guid.NewGuid(), Index = 1, Name = "Chic Elegance", Description = "A collection that embodies timeless style and sophisticated grace." },
    new() { Id = Guid.NewGuid(), Index = 2, Name = "Urban Jungle", Description = "A vibrant and bold collection inspired by the heart of the city." },
    new() { Id = Guid.NewGuid(), Index = 3, Name = "Vintage Charm", Description = "A nostalgic collection that brings the elegance of the past into the present." }, new() { Id = Guid.NewGuid(), Index = 4, Name = "Summer Breeze", Description = "A light and airy collection perfect for warm summer days." },
    new() { Id = Guid.NewGuid(), Index = 5, Name = "Winter Wonderland", Description = "A cozy and chic collection inspired by the magic of winter." },
    new() { Id = Guid.NewGuid(), Index = 6, Name = "Bohemian Rhapsody", Description = "A free-spirited collection with eclectic and artistic designs." },
    new() { Id = Guid.NewGuid(), Index = 7, Name = "Bohemian Rhapsody", Description = "A free-spirited collection with eclectic and artistic designs." },
    new() { Id = Guid.NewGuid(), Index = 8, Name = "Rustic Charm", Description = "A collection that captures the charm of a bygone era." },
    new() { Id = Guid.NewGuid(), Index = 9, Name = "Modern Elegance", Description = "A contemporary collection that exudes sophistication and innovation." },
    new() { Id = Guid.NewGuid(), Index = 11, Name = "Country Road", Description = "A collection inspired by the raw beauty of the open road." },
    new() { Id = Guid.NewGuid(), Index = 10, Name = "Tropical Paradise", Description = "A collection that transports you to a tropical paradise." }
    ];
}