using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api_Mediconnet.Application.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Api_Mediconnet.Infrastructure.Services;

public class JwtTokenIdService : IJwtTokenIdService
{
    private readonly IConfiguration _config;

    public JwtTokenIdService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerarToken(string id, string rol)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Role, rol)
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:KeyTokenId"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(

            issuer: _config["jwt:Emisor"],
            audience: _config["jwt:Receptor"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
