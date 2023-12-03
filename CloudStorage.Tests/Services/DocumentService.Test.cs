

using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudStorage.Constants.ErrorMessages;
using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Services;
using CloudStorage.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CloudStorage.Tests.Services
{
    public class DocumentServiceTests
    {

        private Mock<ICloudinaryService> _cloudinaryServiceMock = new Mock<ICloudinaryService>();
        private Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private Mock<IDocumentRepository> _documentRepositoryMock = new Mock<IDocumentRepository>();

        [Fact]
        public async Task UploadDocument_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var documentService = new DocumentService(
                _cloudinaryServiceMock.Object,
                _userRepositoryMock.Object,
                _documentRepositoryMock.Object
                );

            // Act
            var result = await documentService.UploadDocument(DocumentDtoMock.UploadDocumentDTO, JwtTokenDataMocks.ValidTokenData);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(new List<string>(){
                UploadDocumentErrorMessages.UserDoesNotExist
            }, result.Errors);
        }

        [Fact]
        public async Task GetAllDocumentsByOwnerId_ReturnsDocuments()
        {
            // Arrange
            List<Document> documentsMock = new List<Document>(){
                EntityMocks.Document
            };
            string ownerIdMock = "some-owner-id";

            _documentRepositoryMock.Setup(repo => repo.GetDocumentsByOwner(It.IsAny<string>()))
                .ReturnsAsync(documentsMock);

            var documentService = new DocumentService(
                _cloudinaryServiceMock.Object,
                _userRepositoryMock.Object,
                _documentRepositoryMock.Object
                );

            // Act
            var result = await documentService.GetAllDocumentsByOwnerId(ownerIdMock);

            // Assert
            Assert.Equal(documentsMock, result);
        }
    }
}
