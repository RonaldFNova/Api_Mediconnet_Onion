namespace Api_Mediconnet.Application.interfaces;

public interface IServicioHashPassword
{
    string Hash(string Password);
    bool Verificar(string Password, string PasswordHash);
}