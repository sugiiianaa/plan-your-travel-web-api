using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanYourTravel.Shared.AppSettings;

namespace PlanYourTravel.Infrastructure.Services.TokenGenerator
{
    public sealed class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        public string GenerateToken(Guid userId, string email, string role)
        {
            var jwtSecret = Environment.GetEnvironmentVariable(EnvironmentVariableKey.jwtSecret);

            // TODO : Add validation for environemnt variable
            var securityKeyBytes = Encoding.UTF8.GetBytes(jwtSecret);
            var securityKey = new SymmetricSecurityKey(securityKeyBytes);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role), // For role-based authorization
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // unique token ID
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
