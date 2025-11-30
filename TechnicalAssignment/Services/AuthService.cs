using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public class AuthService
{
    private readonly IConfiguration configuration;
    private readonly AppDbContext dbContext;

    public AuthService(IConfiguration configuration, AppDbContext dbContext)
    {
        this.configuration = configuration;
        this.dbContext = dbContext;
    }

    public TokenDto? Login(UserLoginDto user)
    {
        var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == user.Username);
        var tokenDto = new TokenDto();

        if (userInDb == null)
        {
            return null;
        }

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(userInDb, userInDb.Password, user.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var token = GenerateJwtToken(user.Username);
        tokenDto.Token = token;

        return tokenDto;
    }

    private string GenerateJwtToken(string username)
    {
        // Claims for the token
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var jwtSection = configuration.GetSection("Jwt")
            ?? throw new InvalidOperationException("JWT configuration section missing.");

        var keyValue = jwtSection["Key"]
            ?? throw new InvalidOperationException("JWT Key is missing.");
        var issuer = jwtSection["Issuer"]
            ?? throw new InvalidOperationException("JWT Issuer is missing.");
        var audience = jwtSection["Audience"]
            ?? throw new InvalidOperationException("JWT Audience is missing.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}