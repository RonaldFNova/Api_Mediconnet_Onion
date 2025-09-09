using Microsoft.AspNetCore.Identity;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Services;

public class HashPasswordService : IHashPasswordService
{
    private readonly PasswordHasher<string> hasher = new();

    public string Hash(string Password)
    {
        return hasher.HashPassword("Usuario", Password);
    }

    public bool Verificar(string Password, string PasswordHash)
    {
        var resultado = hasher.VerifyHashedPassword("Usuario", PasswordHash, Password);

        return resultado == PasswordVerificationResult.Success;
    }
}