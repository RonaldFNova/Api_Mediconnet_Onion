namespace Api_Mediconnet.Application.interfaces;

public interface IHashPasswordService
{
    string Hash(string Password);
    bool Verificar(string Password, string PasswordHash);
}