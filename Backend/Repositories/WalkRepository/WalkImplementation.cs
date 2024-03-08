using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.WalkRepository
{
    public class WalkImplementation : IWalkRepository
    {
        private readonly DBContext dBContext;

        public WalkImplementation(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Walks> CreateNewWalk(Walks walks)
        {
            await dBContext.Walk.AddAsync(walks);
            await dBContext.SaveChangesAsync();

            return walks;
        }

        // Using Include method we can get the related data from the database
        public async Task<List<Walks>> GetAllWalks()
        {
            // 1st Way of using Navigation Properties
            return await dBContext.Walk.Include("difficulty").Include("region").ToListAsync();

            // 2nd Way of using Navigation Properties
            // return await dBContext.Walk.Include(x=>x.difficulty).Include(y=> y.region).ToListAsync();
        }

        public async Task<Walks?> GetWalksById(int id)
        {
            return await dBContext.Walk.Include("difficulty").Include("region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks?> UpdateWalks(int id, Walks walks)
        {
            var existingWalk = await dBContext.Walk.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walks.Name;
            existingWalk.Distance = walks.Distance;
            existingWalk.Description = walks.Description;
            existingWalk.ImageURL = walks.ImageURL;
            existingWalk.DifficultyId = walks.DifficultyId;
            existingWalk.RegionId = walks.RegionId;

            await dBContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}