using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    [HttpGet("token")]
    public IActionResult ReadToken()
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

        if(string.IsNullOrEmpty(authHeader))
        {
            return BadRequest("NO authorization header");
        }

        var token = authHeader.Replace("Bearer ", "");

        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        return Ok(new
        {
            Header = jwt.Header,

            Claims = jwt.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
    }
}