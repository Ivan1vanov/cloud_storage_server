using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Models;

namespace CloudStorage.Interfaces
{
    public interface IDocumentService
    {
        public Task<UploadDocumentResult> UploadDocument(UploadDocumentRequestDto data, TokenData jwtTokenData);
        public Task<List<Document>> GetAllDocumentsByOwnerId(string ownerId);
    };
}
