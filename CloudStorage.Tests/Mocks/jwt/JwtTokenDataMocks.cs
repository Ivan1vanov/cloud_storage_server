using CloudStorage.Models;

namespace CloudStorage.Tests.Mocks
{
    public static class JwtTokenDataMocks
    {

        public static TokenData ValidTokenData = new TokenData()
        {
            UserId = "test-user-id",
            Email = "test@user.email"
        };
    }
}