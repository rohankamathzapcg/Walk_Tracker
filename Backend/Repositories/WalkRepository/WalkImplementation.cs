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

        public async Task<Walks?> DeleteWalks(int id)
        {
            var exstingWalk = await dBContext.Walk.FirstOrDefaultAsync(x => x.Id == id);
            if (exstingWalk == null)
            {
                return null;
            }

            dBContext.Walk.Remove(exstingWalk);
            await dBContext.SaveChangesAsync();
            return exstingWalk;
        }

        // Using Include method we can get the related data from the database
        public async Task<List<Walks>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            // 1st Way of using Navigation Properties
            //return await dBContext.Walk.Include("difficulty").Include("region").ToListAsync();

            // 2nd Way of using Navigation Properties
            // return await dBContext.Walk.Include(x=>x.difficulty).Include(y=> y.region).ToListAsync();

            var walks = dBContext.Walk.Include("difficulty").Include("region").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Distance", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Distance) : walks.OrderByDescending(x => x.Distance);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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