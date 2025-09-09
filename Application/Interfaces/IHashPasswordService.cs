namespace Api_Mediconnet.Application.Interfaces;

public interface IHashPasswordService
{
    string Hash(string Password);
    bool Verificar(string Password, string PasswordHash);
}