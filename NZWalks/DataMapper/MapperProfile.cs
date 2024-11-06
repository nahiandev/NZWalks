using AutoMapper;
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
        }
    }
}
