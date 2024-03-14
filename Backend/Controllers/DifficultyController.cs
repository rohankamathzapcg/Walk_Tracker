using AutoMapper;
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
    }
}
