using Microsoft.AspNetCore.Identity;
using Api_Mediconnet.Application.interfaces;

namespace Api_Mediconnet.Infrastructure.Services;

public class HashPasswordService : IHashPasswordService
{
    private readonly PasswordHasher<string> hasher = new();

    public string Hash(string Password)
    {
        return hasher.HashPassword(null, Password);
    }

    public bool Verificar(string Password, string PasswordHash)
    {
        var resultado = hasher.VerifyHashedPassword(null, PasswordHash, Password);

        return resultado == PasswordVerificationResult.Success;

    }
}