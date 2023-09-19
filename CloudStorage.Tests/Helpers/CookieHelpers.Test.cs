using System;
using CloudStorage.Constants;
using CloudStorage.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CloudStorage.Tests.Helpers
{
    public class CookieHelpersTests
    {

        private Mock<IRequestCookieCollection> _cookieCollectionMock = new Mock<IRequestCookieCollection>();

        [Fact]
        public void GetJwtTokenFromCookies_ValidToken_ReturnsToken()
        {
            // Arrange
            var jwtTokenMock = "header.payload.signature";
            _cookieCollectionMock.Setup(c => c[CookieKeyNames.access_token]).Returns(jwtTokenMock);

            // Act
            var result = CookieHelpers.GetJwtTokenFromCookies(_cookieCollectionMock.Object);

            // Assert
            Assert.Equal(jwtTokenMock, result);
        }

        [Fact]
        public void GetJwtTokenFromCookies_MissingToken_ThrowsException()
        {
            // Arrange
            _cookieCollectionMock.Setup(c => c[CookieKeyNames.access_token]).Returns((string)null);

            // Assert
            var exeption = Assert.Throws<ApplicationException>(() => CookieHelpers.GetJwtTokenFromCookies(_cookieCollectionMock.Object));
            Assert.Equal(CookieHelpersErrorMessages.canNotGetAccesTokenFromCookies, exeption.Message);
        }
    }
}