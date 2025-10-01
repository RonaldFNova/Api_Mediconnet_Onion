namespace Api_Mediconnet.Application.DTOs;

public class StatusCodeDTO
{
    public int StatusCode { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public string? Token { get; set; }
    public bool? VerificadoEmail { get; set; }
}