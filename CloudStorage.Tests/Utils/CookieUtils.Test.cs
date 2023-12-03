using CloudStorage.Utils;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CloudStorage.Tests.Utils
{
    public class CookieUtilsTests
    {

        private Mock<IRequestCookieCollection> _cookieCollectionMock = new Mock<IRequestCookieCollection>();

        [Fact]
        public void GetJwtTokenFromCookies_MissingToken_ThrowsException()
        {
            // Arrange
            var result = CookieUtils.GetAuthCookieOptions();

            // Assert
            Assert.False(result.HttpOnly);
            Assert.Equal(SameSiteMode.Strict, result.SameSite);
        }
    }
}