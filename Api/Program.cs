using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;
using DotNetEnv;


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


builder.Services.AddControllers();

Env.Load();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(_secretConnectDb,ServerVersion.AutoDetect(_secretConnectDb),
    b => b.MigrationsAssembly("Api_Mediconnet.Infrastructure"))
    );

var app = builder.Build();


// Crear la base de datos (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); 
    Console.WriteLine("Base de datos verificada o creada correctamente.");
}


app.MapControllers();

app.Run();

