using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Repositories.RegionRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dbContext;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }
        /* Using DTO to Expose to the client and Models to map to the Database */

        // Getting All Regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            // Get Data From DataBase - Domain models

            // Without using Repository Layer:- var regionsDomain = await _dbContext.Regions.ToListAsync();
            
            var regionsDomain = await _regionRepository.GetAllRegions();
            
            // Map Domain Models to DTOs
            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    ImageURL = region.ImageURL,
                });
            }

            /*var regions = new List<Region>()
            {
                new Region
                {
                    Id = 1,
                    Name = "Auckland Region",
                    Code="AKL",
                    ImageURL="https://cdn.britannica.com/99/61399-050-B867F67F/skyline-Auckland-New-Zealand-Westhaven-Marina.jpg"
                },
                new Region
                {
                    Id = 2,
                    Name="Wellington Region",
                    Code="WLG",
                    ImageURL="https://cdn.britannica.com/99/61399-050-B867F67F/skyline-Auckland-New-Zealand-Westhaven-Marina.jpg"
                }
            };*/

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
            
            //Without using Repository Layer:- var regionModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionModel = await _regionRepository.GetRegionById(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            // Map Region Model to Region DTO
            var regionsDTO = new RegionDTO
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                ImageURL = regionModel.ImageURL
            };

            // Sending the DTO to Client
            return Ok(regionsDTO);
        }

        // To Create new Region
        [HttpPost]
        public async Task<IActionResult> CreateNewRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                ImageURL = addRegionRequestDTO.ImageURL
            };

            // Use Domain Model to Create Region
            regionDomainModel=await _regionRepository.CreateRegion(regionDomainModel);

            // Map Domain model Back to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageURL = regionDomainModel.ImageURL
            };

            return CreatedAtAction(nameof(GetRegionsById), new { id = regionDTO.Id }, regionDTO);
        }

        // Update Region
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Convert Domain Model into DTO
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                ImageURL = updateRegionRequestDTO.ImageURL
            };
            regionDomainModel = await _regionRepository.UpdateRegion(id, regionDomainModel);
            
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageURL = regionDomainModel.ImageURL
            };

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
            var regionData = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                ImageURL = regionDomainModel.ImageURL
            };
            return Ok(regionData);

        }
    }
}
