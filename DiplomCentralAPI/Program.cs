using DiplomCentralAPI.Data;
using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot _dbConfig = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<DBContext>(options => options.UseNpgsql(_dbConfig.GetConnectionString("PostgreSQL13")));

builder.Services.AddScoped<IRepository<Handler>, DiplomCentralAPI.Data.Repository.Postgres.HandlerRepository>();
builder.Services.AddScoped<IRepository<Schema>, DiplomCentralAPI.Data.Repository.Postgres.SchemaRepository>();
builder.Services.AddScoped<IRepository<Experiment>, DiplomCentralAPI.Data.Repository.Postgres.ExperimentRepository>();
builder.Services.AddScoped<IRepository<Photo>, DiplomCentralAPI.Data.Repository.Postgres.PhotoRepository>();

builder.Services.AddMvc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
