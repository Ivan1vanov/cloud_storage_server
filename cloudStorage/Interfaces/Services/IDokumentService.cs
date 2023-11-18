using CloudStorage.DTOs;
using CloudStorage.Models;

namespace CloudStorage.Interfaces
{
    public interface IDocumentService
    {
        public Task<UploadDocumentResult> UploadDocument(UploadDocumentRequestDto data, TokenData jwtTokenData);
    };
}
