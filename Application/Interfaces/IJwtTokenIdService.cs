namespace Api_Mediconnet.Application.interfaces;

public interface IJwtTokenIdService
{
    string GenerarToken(string id, string rol);
}