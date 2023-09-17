using Xunit;
using Microsoft.AspNetCore.Http;
using CloudStorage.Helpers;

namespace CloudStorage.Tests.HttpHelpersTests
{
    public class HttpHelpersTests
    {
        [Fact]
        public void GetJwtTokenFromHeaders_ReturnsToken()
        {
            // Arrange
            string jwtTokenMock = "headers.payload.signature";
            var headers = new HeaderDictionary();
            headers["Authorization"] = $"Bearer {jwtTokenMock}";

            // Act
            string result = HttpHelpers.GetJwtTokenFromHeaders(headers);

            // Assert
            Assert.Equal(jwtTokenMock, result);
        }
    }
}