using CloudStorage.DTOs;
using CloudStorage.Models;

namespace CloudStorage.Interfaces{
    public interface IDokumentService {
        public Task<UploadDokumentResult> UploadDokument(UploadDokumentRequestDto data, TokenData jwtTokenData);
    };
}