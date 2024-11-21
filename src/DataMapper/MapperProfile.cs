using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;


namespace NZWalks.DataMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
            CreateMap<UpdateRegionDTO, RegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
            CreateMap<AddWalkDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();
            CreateMap<IdentityUser, RegisterDTO>().ReverseMap();
        }
    }
}
