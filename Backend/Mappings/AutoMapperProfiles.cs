using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        }
    }
}
