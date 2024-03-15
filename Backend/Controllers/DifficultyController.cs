using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Repositories.DifficultyRepository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDificulties()
        {
            var difficultyDomain = await difficultyRepository.GetAllDifficulies();

            var difficultyDTO = mapper.Map<List<DifficultyDTO>>(difficultyDomain);

            return Ok(difficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDifficulty([FromBody] DifficultyDTO difficultyDTO)
        {
            if (ModelState.IsValid)
            {
                var difficultyDomain = mapper.Map<Difficulty>(difficultyDTO);
                difficultyDomain = await difficultyRepository.CreateDifficulty(difficultyDomain);
                var difficultyDTO = mapper.Map<DifficultyDTO>(difficultyDomain);
                return Ok(difficultyDTO);

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
