using Backend.Data;
using Backend.Mappings;
using Backend.Models;
using Backend.Repositories.RegionRepository;
using Backend.Repositories.WalkRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*************************************************/

// Add services to the container.
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiPostgresDatabase")));

// Injecting Automapper class
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Injecting Region Repository class with Interface
builder.Services.AddScoped<IRegionRepository, RegionImplemetation>();

// Injecting Walks Repository class with Interface
builder.Services.AddScoped<IWalkRepository, WalkImplementation>();

/*************************************************/

// Injecting Controller class
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
