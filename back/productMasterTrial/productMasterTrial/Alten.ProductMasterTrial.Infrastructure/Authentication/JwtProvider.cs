using Alten.ProductMaster.Application.Common.Authentication;
using Alten.ProductMasterTrial.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alten.ProductMaster.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(Member member)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, member.Email.Value),
                new(ClaimTypes.Email, member.Email.Value), 
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: null,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }
    }
}
