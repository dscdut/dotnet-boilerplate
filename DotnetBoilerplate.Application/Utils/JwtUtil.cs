using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DotnetBoilerplate.Domain.Dtos;
using DotnetBoilerplate.Domain.Payloads;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetBoilerplate.Infrastructure.Utils
{
    public class JwtUtil
    {
        private static JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        private static SecurityToken GenerateToken(UserDto user, string secretKey, double expirationDays)
        {
            
            var key = GetSymmetricSecurityKey(secretKey);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                SigningCredentials = creds
            };

            return tokenHandler.CreateToken(tokenDescriptor);

        }

        public static TokenPayload GenerateAccessToken(UserDto user, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret").Value;
            var expirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime").Value);
            SecurityToken accessToken = GenerateToken(user, secretKey, expirationTime);
            var token = new TokenPayload();
            token.Access = tokenHandler.WriteToken(accessToken);
            return token;
        }

        public static TokenPayload GenerateAccessAndRefreshToken(UserDto user, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret").Value;
            var accessTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime").Value);
            var refreshTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:RefreshTokenExpirationTime").Value);
            SecurityToken accessToken = GenerateToken(user, secretKey, accessTokenExpirationTime);
            SecurityToken refreshToken = GenerateToken(user, secretKey, refreshTokenExpirationTime);
            var token = new TokenPayload();
            token.Access = tokenHandler.WriteToken(accessToken);
            token.Refresh = tokenHandler.WriteToken(refreshToken);
            return token;
        }
    }
}
