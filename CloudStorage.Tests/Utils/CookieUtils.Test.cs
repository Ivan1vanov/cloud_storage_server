using System;
using CloudStorage.Constants;
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
        public void GetJwtTokenFromCookies_ValidToken_ReturnsToken()
        {
            // Arrange
            var jwtTokenMock = "header.payload.signature";
            _cookieCollectionMock.Setup(c => c[CookieKeyNames.access_token]).Returns(jwtTokenMock);

            // Act
            var result = CookieUtils.GetJwtTokenFromCookies(_cookieCollectionMock.Object);

            // Assert
            Assert.Equal(jwtTokenMock, result);
        }

        [Fact]
        public void GetJwtTokenFromCookies_MissingToken_ThrowsException()
        {
            // Arrange
            _cookieCollectionMock.Setup(c => c[CookieKeyNames.access_token]).Returns((string)null);

            // Assert
            var exeption = Assert.Throws<ApplicationException>(() => CookieUtils.GetJwtTokenFromCookies(_cookieCollectionMock.Object));
            Assert.Equal(CookieUtilsErrorMessages.canNotGetAccesTokenFromCookies, exeption.Message);
        }
    }
}