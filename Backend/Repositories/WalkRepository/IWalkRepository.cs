using Backend.Models;

namespace Backend.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        public Task<Walks> CreateNewWalk(Walks walks);
        public Task<List<Walks>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pigeSize = 1000);
        public Task<Walks?> GetWalksById(int id);
        public Task<Walks?> UpdateWalks(int id, Walks walks);
        public Task<Walks?> DeleteWalks(int id);
    }
}
