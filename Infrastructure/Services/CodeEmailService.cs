namespace Api_Mediconnet.Infrastructure.Services;

public class CodeEmailService
{
    public string GenerateCode()
    {
        Random random = new();
        return random.Next(100000, 999999).ToString();
    }
}