using System.Net;
using CloudStorage.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CloudStorage.DTOs
{
    public static class DocumentDtoMock
    {
        public static UploadDocumentRequestDto UploadDocumentDTO = new UploadDocumentRequestDto()
        {
            Description = "test description",
            File = new Mock<IFormFile>().Object,
        };
    }

    public static class CloudinaryDataMock
    {
        public static UploadCloudinaryFileResult UploadCloudinaryFileSuccessfulResult = new UploadCloudinaryFileResult()
        {
            DocumentId = new System.Guid("ab23d71d-f41d-42df-9bad-504ec2880d0e"),
            DocumentName = "successful-uploaded-document",
            StatusCode = HttpStatusCode.OK,
            DocumentExtension = ".docs"
        };

        public static UploadCloudinaryFileResult UploadCloudinaryFileUnsuccessfulResult = new UploadCloudinaryFileResult()
        {
            DocumentId = new System.Guid("f886ebb2-9147-11ee-b9d1-0242ac120002"),
            DocumentName = "unsuccessful-uploaded-document",
            StatusCode = HttpStatusCode.InternalServerError,
            DocumentExtension = ".docs"
        };
    }
}