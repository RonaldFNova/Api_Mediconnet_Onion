namespace Api_Mediconnet.Application.DTOs;

public class ValidarCodigoVerificacionResponseDTO
{
    public int StatusCode { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public string? Token { get; set; }

}