using Backend.Data;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllRegions()
        {
            // Get Data From DataBase - Domain models
            var regionsDomain = _dbContext.Regions.ToList();

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
    }
}
