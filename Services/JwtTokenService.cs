using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CloudStorage.Config;
using CloudStorage.Constants;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CloudStorage.Services
{
    public class JwtTokenService : IJwtTokenService {
        JwtConfig _jwtConfig;

    public JwtTokenService(IOptions<JwtConfig> jwtConfig) {
        _jwtConfig = jwtConfig.Value;
    } 

    public TokenData DecodeToken(string token)
    {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            return new TokenData(){
                UserId = jwtToken.Payload[JwtTokenPayloadKeyNames.user_id].ToString(),
                Email = jwtToken.Payload[JwtTokenPayloadKeyNames.email].ToString(),
            };
    }

    public string GenerateAccessToken(GenerateJwtTokenPayload data) 
    {
    var jwtTokenHandler = new JwtSecurityTokenHandler();

    var keyBytes = Encoding.UTF8.GetBytes(_jwtConfig.AccessTokenKey);
    var tokenDescriptor = new SecurityTokenDescriptor(){
        Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtTokenPayloadKeyNames.user_id, data.UserId.ToString()),
                new Claim(JwtTokenPayloadKeyNames.email, data.Email),
                new Claim(JwtRegisteredClaimNames.Sub, data.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            }
        ),
        Expires = DateTime.Now.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = jwtTokenHandler.WriteToken(token);
    return jwtToken;
    }
    }
}