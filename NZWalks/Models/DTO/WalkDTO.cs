﻿using NZWalks.Models.Domain;

namespace NZWalks.Models.DTO
{
    public class WalkDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // public Difficulty Difficulty { get; set; }
        // public Region Region { get; set; }

    }
}
