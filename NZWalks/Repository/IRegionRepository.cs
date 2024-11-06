﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Guid id);
        Task<Region?> DeleteAsync(Guid id);

        // public IActionResult Update(Guid id, UpdateRegionDTO new_region)

        Task<RegionDTO> UpdateAsync(Guid id, UpdateRegionDTO new_region);
    }
}
