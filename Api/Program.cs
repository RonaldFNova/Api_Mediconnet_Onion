using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;
using DotNetEnv;
using Api_Mediconnet.Application.Services;
using Api_Mediconnet.Infrastructure.Repositories;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


string secretConnectDbPath = "/etc/secrets/CONNECTION_STRING";
string _secretConnectDb = "";

if (File.Exists(secretConnectDbPath))
{
    _secretConnectDb = File.ReadAllText(secretConnectDbPath).Trim();
    Console.WriteLine($"Clave cargada desde archivo: {secretConnectDbPath}");
}

else
{
    Env.Load(Path.Combine(builder.Environment.ContentRootPath, ".env"));
    _secretConnectDb = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
    Console.WriteLine("Clave cargada desde variables de entorno");
}

if (string.IsNullOrWhiteSpace(_secretConnectDb))
{
    throw new InvalidOperationException(

        "Variables de entorno no encontradas"
    ); 
}


Env.Load();

builder.Configuration.AddEnvironmentVariables();

var jwtKey = builder.Configuration["Jwt:KeyTokenId"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey!);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // solo en local.
            ValidateAudience = false, // solo en local.
            ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration["Jwt__Issuer"], 
            //ValidAudience = builder.Configuration["Jwt__Audience"], 
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddScoped<ITUsuariosService, TUsuariosService>();
builder.Services.AddScoped<ITUsuariosRepository, TUsuarioRepository>();

builder.Services.AddScoped<ITEstadoUsuarioService, TEstadoUsuarioService>();
builder.Services.AddScoped<ITEstadoUsuarioRepository, TEstadoUsuarioRepository>();

builder.Services.AddScoped<ITRolService, TRolService>();
builder.Services.AddScoped<ITRolRepository, TRolRepository>();

builder.Services.AddScoped<ITEstadoVerificacionService, TEstadoVerificacionService>();
builder.Services.AddScoped<ITEstadoVerificacionRepository, TEstadoVerificacionRepository>();

builder.Services.AddScoped<ITipoIdentificacionService, TTipoIdentificacionService>();
builder.Services.AddScoped<ITTipoIdentificacionRepository, TTipoIdentificacionRepository>();

builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();

builder.Services.AddScoped<IJwtTokenIdService, JwtTokenIdService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(_secretConnectDb,ServerVersion.AutoDetect(_secretConnectDb),
    b => b.MigrationsAssembly("Api_Mediconnet.Infrastructure"))
    );

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

// Crear la base de datos (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    Console.WriteLine("Base de datos verificada o creada correctamente.");
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

