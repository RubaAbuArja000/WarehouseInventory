using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warehouse.Core.Entities;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class AuthService(IUserRepository _userRepository, JWTSettings _jwtSettings) : IAuthService
{
    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email)
                   ?? throw new Exception("Invalid email or password");

        if (!user.IsActive)
            throw new Exception("Your account is disabled. Contact support.");

        var isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash!);
        if (!isValid)
            throw new Exception("Invalid email or password");

        return GenerateJwt(user);
    }
    private string GenerateJwt(User user)
    {
        var secretKey = _jwtSettings.Key;
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("FullName", user.FullName)
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}