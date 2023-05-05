using DiplomCentralAPI.Data;
using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using DiplomCentralAPI.Data.Repository.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot _dbConfig = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("appsettings.json").Build();

builder.Services.AddSession();
builder.Services.AddControllers();

builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(_dbConfig.GetConnectionString("PostgreSQL13")));

builder.Services.AddScoped<IRepository<MyHandler>, HandlerRepository>();
builder.Services.AddScoped<IRepository<Schema>, SchemaRepository>();
builder.Services.AddScoped<IRepository<Experiment>, ExperimentRepository>();
builder.Services.AddScoped<IRepository<Photo>, PhotoRepository>();

builder.Services.AddMvc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
