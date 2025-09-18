using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;
using DotNetEnv;
using Api_Mediconnet.Application.Services;
using Api_Mediconnet.Infrastructure.Repositories;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api_Mediconnet.Api.Middleware;
using System.Text;
using Microsoft.Extensions.Logging.Console;
using Serilog;


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

string jwtKey = builder.Configuration["Jwt:KeyTokenId"]!;
string validIssuer = builder.Configuration["Jwt:Issuer"]!;
string validateAudience = builder.Configuration["Jwt:Audience"]!;


if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(validIssuer) || string.IsNullOrWhiteSpace(validateAudience))
{
    throw new InvalidOperationException("Variables de entorno no encontradas");
}

var ApiKeySendGrid = Environment.GetEnvironmentVariable("BREVO_API_KEY");
if (string.IsNullOrWhiteSpace(ApiKeySendGrid))
{
    throw new InvalidOperationException("BREVO_API_KEY no encontrada");
}

var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, 
            ValidateAudience = true, 
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer, 
            ValidAudience = validateAudience, 
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });



// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    // logs generales (ASP.NET, EF, etc.)
    .WriteTo.File("Logs/system-.log", rollingInterval: RollingInterval.Day)
    // logs de tu namespace (ejemplo Api_Mediconnet.*)
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e =>
            e.Properties.ContainsKey("SourceContext") &&
            e.Properties["SourceContext"].ToString().Contains("Api_Mediconnet"))
        .WriteTo.File("Logs/business-.log", rollingInterval: RollingInterval.Day))
    .CreateLogger();
    

builder.Host.UseSerilog();


builder.Logging.ClearProviders();

builder.Logging.AddConsole(options =>
{
    options.FormatterName = ConsoleFormatterNames.Simple;
});

builder.Logging.AddSimpleConsole(options =>
{
    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
    options.SingleLine = true; // opcional: logs en una sola línea
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

builder.Services.AddScoped<ITUsuarioService, TUsuarioService>();
builder.Services.AddScoped<ITUsuarioRepository, TUsuarioRepository>();

builder.Services.AddScoped<ITEstadoUsuarioervice, TEstadoUsuarioervice>();
builder.Services.AddScoped<ITEstadoUsuarioRepository, TEstadoUsuarioRepository>();

builder.Services.AddScoped<ITRolService, TRolService>();
builder.Services.AddScoped<ITRolRepository, TRolRepository>();

builder.Services.AddScoped<ITEstadoVerificacionService, TEstadoVerificacionService>();
builder.Services.AddScoped<ITEstadoVerificacionRepository, TEstadoVerificacionRepository>();

builder.Services.AddScoped<ITipoIdentificacionService, TTipoIdentificacionService>();
builder.Services.AddScoped<ITTipoIdentificacionRepository, TTipoIdentificacionRepository>();

builder.Services.AddScoped<ITLoginsService, TLoginsService>();
builder.Services.AddScoped<ITLoginsRepository, TLoginsRepository>();

builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();

builder.Services.AddScoped<IJwtTokenIdService, JwtTokenIdService>();

builder.Services.AddScoped<ITGrupoSanguineoService, TGrupoSanguineoService>();
builder.Services.AddScoped<ITGrupoSanguineoRepository, TGrupoSanguineoRepository>();

builder.Services.AddScoped<ITPersonaService, TPersonaService>();
builder.Services.AddScoped<ITPersonaRepository, TPersonaRepository>();

builder.Services.AddScoped<ITPacienteService, TPacienteService>();
builder.Services.AddScoped<ITPacienteRepository, TPacienteRepository>();

builder.Services.AddScoped<ITEspecialidadService, TEspecialidadService>();
builder.Services.AddScoped<ITEspecialidadRepository, TEspecialidadRepository>();

builder.Services.AddScoped<ITProfesionalService, TProfesionalService>();
builder.Services.AddScoped<ITProfesionalRepository, TProfesionalRepository>();

builder.Services.AddScoped<ITAreaService, TAreaService>();
builder.Services.AddScoped<ITAreaRepository, TAreaRepository>();

builder.Services.AddScoped<ITEstadoCitaService, TEstadoCitaService>();
builder.Services.AddScoped<ITEstadoCitaRepository, TEstadoCitaRepository>();

builder.Services.AddScoped<ITCitaService, TCitaService>();
builder.Services.AddScoped<ITCitaRepository, TCitaRepository>();

builder.Services.AddScoped<ITCodigoVerificacionService, TCodigoVerificacionService>();
builder.Services.AddScoped<ITCodigoVerificacionRepository, TCodigoVerificacionRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<CodeEmailService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(_secretConnectDb,ServerVersion.AutoDetect(_secretConnectDb),
    b => b.MigrationsAssembly("Api_Mediconnet.Infrastructure"))
    );

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>(); 

app.UseCors(MyAllowSpecificOrigins);

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

