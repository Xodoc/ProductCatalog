using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCatalog.Api.Features.Auth.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<Result<string>> GenerateTokenAsync(User user)
        {
            var tokenDescriptor = await GetTokenDescriptorAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Result.Success(token);
        }

        private async Task<SecurityTokenDescriptor> GetTokenDescriptorAsync(User user)
        {
            var userClaims = await GetUserClaimsAsync(user);
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(int.Parse(_config["Jwt:LifeTimeInDays"]!)),
                SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            };
        }

        private async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Email!)
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return authClaims;
        }
    }
}
