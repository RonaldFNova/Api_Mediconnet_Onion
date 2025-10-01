namespace Api_Mediconnet.Application.DTOs;

public class PasswordResetDTO
{
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}