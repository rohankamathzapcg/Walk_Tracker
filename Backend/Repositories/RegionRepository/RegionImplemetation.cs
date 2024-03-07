using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.RegionRepository
{
    public class RegionImplemetation : IRegionRepository
    {
        private readonly NZWalksDBContext _dBContext;
        public RegionImplemetation(NZWalksDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Region> CreateRegion(Region region)
        {
            await _dBContext.Regions.AddAsync(region);
            await _dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegion(int id)
        {
            var existingRegion = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            _dBContext.Regions.Remove(existingRegion);
            await _dBContext.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<List<Region>> GetAllRegions()
        {
            return await _dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionById(int id)
        {
            return await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateRegion(int id, Region region)
        {
            var existingRegion= await _dBContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code=region.Code;
            existingRegion.Name=region.Name; 
            existingRegion.ImageURL=region.ImageURL;

            await _dBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
