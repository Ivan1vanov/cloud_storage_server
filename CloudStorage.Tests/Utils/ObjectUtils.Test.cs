using System.Collections.Generic;
using CloudStorage.Utils;
using Xunit;

namespace CloudStorage.Tests.Utils
{
    public class ObjectUtilsTests {
        [Fact]
        public void CheckForNullProperties_InputHasNullPreparties_ReturnBooleanAndNullPropertieNames() 
        {
            // Arrange
            var inputMock = new{
                name = "test",
                surname = (string?)null,
                email = (string?)null,
            };

            // act
            var result = ObjectUtils.CheckForNullProperties(inputMock);

            // Assert
            Assert.True(result.HasNullProperties);
            Assert.Equal(new List<string>{ "surname", "email" }, result.NullProperties);
        }

        [Fact]
        public void CheckForNullProperties_InputDoesNotHaveNullProperties_ReturnBoolan()
        {
            // Arrange
            var inputMock = new{
                name = "test",
                surname = "test-surname",
                email = "test-email",
            };

            // act
            var result = ObjectUtils.CheckForNullProperties(inputMock);

            // Assert
            Assert.False(result.HasNullProperties);
            Assert.Null(result.NullProperties);
        }
    }
}