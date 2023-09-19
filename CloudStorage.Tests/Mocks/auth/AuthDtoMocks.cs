using CloudStorage.DTOs;

namespace CloudStorage.Tests.Mocks
{
    public static class AuthDtoMocks
    {
        public static UserSighUpRequestDto UserSignUpRequestDtoMock = new UserSighUpRequestDto()
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "1123",
        };

        public static UserSignInRequestDto UserSignInRequestDtoMock = new UserSignInRequestDto()
        {
            Email = "test@test.com",
            Password = "1123",
        };
    }
}