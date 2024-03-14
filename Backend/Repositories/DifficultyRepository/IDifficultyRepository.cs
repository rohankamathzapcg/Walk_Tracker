using Backend.Models;

namespace Backend.Repositories.DifficultyRepository
{
    public interface IDifficultyRepository
    {
        public Task<List<Difficulty>> GetAllDifficulies();
    }
}
