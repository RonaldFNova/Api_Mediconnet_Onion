namespace Api_Mediconnet.Application.DTOs;

public class LoginResponseDTO
{
    public int StatusCode { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}