using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dbContext;
        public RegionsController(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        /* Using DTO to Expose to the client and Models to map to the Database */

        // Getting All Regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            // Get Data From DataBase - Domain models
            var regionsDomain = await _dbContext.Regions.ToListAsync();

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
        public IActionResult GetRegionsById(int id)
        {
            // Get Single Data from Database - Domain models

            // var regionId = _dbContext.Regions.Find(id); only used for Id (Primary Key) 
            // Using Linq
            var regionModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
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
        public IActionResult CreateNewRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                ImageURL = addRegionRequestDTO.ImageURL
            };

            // Use Domain Model to Create Region
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

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
        public IActionResult UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.ImageURL = updateRegionRequestDTO.ImageURL;

            _dbContext.SaveChanges();

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
        public IActionResult DeleteRegion([FromRoute] int id)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();

            return Ok();

        }
    }
}
