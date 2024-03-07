using Backend.Models;

namespace Backend.Repositories.RegionRepository
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllRegions();
        public Task<Region?> GetRegionById(int id);
        public Task<Region> CreateRegion(Region region);
        public Task<Region?> UpdateRegion(int id, Region region);
        public Task<Region?> DeleteRegion(int id);
    }
}
