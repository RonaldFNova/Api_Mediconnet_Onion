namespace Api_Mediconnet.Application.Interfaces;

public interface IJwtTokenIdService
{
    string GenerarToken(string id, string rol);
}