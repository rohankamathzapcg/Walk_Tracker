using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // For Regions
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();

            // For Walks
            CreateMap<Walks, WalkDTO>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walks>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walks>().ReverseMap();  

            // For Dificulty
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<AddDifficultyRequestDTO, Difficulty>().ReverseMap();
        }
    }
}
