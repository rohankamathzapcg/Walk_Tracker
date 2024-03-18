using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Repositories.RegionRepository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;

        // No use as Dbcontext is defined in Reppository to maintain Abstarction
        //private readonly NZWalksDBContext _dbContext;

        //public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository)
        //{
        //    _dbContext = dbContext;
        //    _regionRepository = regionRepository;
        //}
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            this.mapper = mapper;
        }

        /* Using DTO to Expose to the client and Models to map to the Database */

        // Getting All Regions
        [HttpGet]

        // Block Unauthorized users
        // [Authorize]
        public async Task<IActionResult> GetAllRegions()
        {
            // Get Data From DataBase - Domain models

            // Without using Repository Layer:- var regionsDomain = await _dbContext.Regions.ToListAsync();

            var regionsDomain = await _regionRepository.GetAllRegions();

            // Map Domain Models to DTOs
            /* var regionsDTO = new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    ImageURL = region.ImageURL,
                });
            } */

            // Using Automapper :- Map Domain Models to DTOs
            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);

            // Sending the DTO to Client
            return Ok(regionsDTO);
        }

        // Getting Single Region By ID
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRegionsById(int id)
        {
            // Get Single Data from Database - Domain models

            // var regionId = _dbContext.Regions.Find(id); only used for Id (Primary Key) 
            // Using Linq

            // Without using Repository Layer:- var regionModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionModel = await _regionRepository.GetRegionById(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            // Map Region Model to Region DTO
            /* var regionsDTO = new RegionDTO
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                ImageURL = regionModel.ImageURL
            }; */

            // Using Automapper :- Map Domain Models to DTOs
            var regionsDTO = mapper.Map<RegionDTO>(regionModel);

            // Sending the DTO to Client
            return Ok(regionsDTO);
        }

        // To Create new Region
        [HttpPost]
        public async Task<IActionResult> CreateNewRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            if (ModelState.IsValid)
            {
                // Map or Convert DTO to Domain Model
                /* var regionDomainModel = new Region
                {
                    Name = addRegionRequestDTO.Name,
                    Code = addRegionRequestDTO.Code,
                    ImageURL = addRegionRequestDTO.ImageURL
                }; */

                // Using Automapper :- Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

                // Use Domain Model to Create Region
                regionDomainModel = await _regionRepository.CreateRegion(regionDomainModel);

                // Map Domain model Back to DTO
                /* var regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    Code = regionDomainModel.Code,
                    ImageURL = regionDomainModel.ImageURL
                }; */

                // Using Automapper :- Map Domain Model to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetRegionsById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Update Region
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Convert Domain Model into DTO
            /* var regionDomainModel = new Region
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                ImageURL = updateRegionRequestDTO.ImageURL
            }; */

            // Using Automapper :- Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

            regionDomainModel = await _regionRepository.UpdateRegion(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            /* var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageURL = regionDomainModel.ImageURL
            }; */

            // Using Automapper :- Map Domain Model to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            var regionDomainModel = await _regionRepository.DeleteRegion(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted Region back

            // Map Domian to DTO
            /* var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageURL = regionDomainModel.ImageURL
            }; */

            // Using Automapper :- Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);

        }
    }
}