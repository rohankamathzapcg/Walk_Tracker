using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Repositories.WalkRepository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create New Walk
        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Using Automapper:- Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walks>(addWalkRequestDTO);

            await walkRepository.CreateNewWalk(walkDomainModel);

            // Using Automapper:- Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        // Get All Walks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDomainModel = await walkRepository.GetAllWalks();

            // Using Automapper:- Map Domain Model to DTO
            var walkDTO = mapper.Map<List<WalkDTO>>(walkDomainModel);
            return Ok(walkDTO);

        }

        // Get walk details by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWalkById([FromRoute] int id)
        {
            var walkDomainModel = await walkRepository.GetWalksById(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Using Automapper:- Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        // Update walk by Id
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] int id, [FromBody]UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            // Using Automapper:- Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walks>(updateWalkRequestDTO);

            walkDomainModel = await walkRepository.UpdateWalks(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Using Automapper:- Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }
    }
}
