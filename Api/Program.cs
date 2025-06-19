using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

    
var app = builder.Build();

app.MapControllers();

app.Run();

