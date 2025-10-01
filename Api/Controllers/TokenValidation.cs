using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Auth/TokenJWT")]
public class TokenValidation : ControllerBase
{

    [HttpPost]
    [Authorize]
    public IActionResult PostValidarTokenAsync()
    {
        return Ok(new { Mensaje = "Token valido" });
    }
} 