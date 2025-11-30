using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto user)
    {
        var tokenDto = authService.Login(user);

        if (tokenDto != null)
        {
            return Ok(tokenDto.Token);
        }
        return Unauthorized();
    }
}