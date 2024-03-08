using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walks> Walk { get; set; }

        // Used for Seeding the data into Database

        // In Package Manager console
        // Add-Migration "Seed Data for Difficulties and Regions"
        // update-Database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for difficulties: Easy, Medium , Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = 1,
                    Name="Easy",
                },
                new Difficulty()
                {
                    Id=2,
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id=3,
                    Name="Hard"
                }
            };
            // Seed difficulties into the Database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id=1,
                    Name="Auckland Region",
                    Code="AKL",
                    ImageURL="123.png"
                },
                new Region()
                {
                    Id=2,
                    Name="Udupi Region",
                    Code="KA20",
                    ImageURL="123.png"
                },
                new Region()
                {
                    Id=3,
                    Name="Mangalore Region",
                    Code="KA19",
                    ImageURL="123.png"
                }
            };

            // Seed Regions into Databse
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
