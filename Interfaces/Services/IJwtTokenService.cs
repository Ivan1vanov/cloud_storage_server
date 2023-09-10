using CloudStorage.Models;

namespace CloudStorage.Interfaces
{
    public interface IJwtTokenService {
        public TokenData DecodeToken(string token);
        public string GenerateAccessToken(GenerateJwtTokenPayload data);
    }
}