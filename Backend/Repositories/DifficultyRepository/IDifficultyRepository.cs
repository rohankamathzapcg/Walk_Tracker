using Backend.Models;

namespace Backend.Repositories.DifficultyRepository
{
    public interface IDifficultyRepository
    {
        public Task<Difficulty> CreateDifficulty(Difficulty difficulty);
        public Task<List<Difficulty>> GetAllDifficulies();
        public Task<Difficulty> UpdateDifficulty(int id, Difficulty difficulty);
    }
}
