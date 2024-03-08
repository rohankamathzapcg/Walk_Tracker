using Backend.Models;

namespace Backend.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        public Task<Walks> CreateNewWalk(Walks walks);
        public Task<List<Walks>> GetAllWalks();
        public Task<Walks?> GetWalksById(int id);
        public Task<Walks?> UpdateWalks(int id, Walks walks);
    }
}
