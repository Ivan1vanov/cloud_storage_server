using System.Net;
using System.Text.Json;
using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Models;

namespace CloudStorage.Services
{
    public class DocumentService : IDocumentService
    {

        private ICloudinaryService _cloudinaryService;
        private IUserRepository _userRepository;
        private IDocumentRepository _documentRepository;

        public DocumentService(
            ICloudinaryService cloudinaryService,
            IUserRepository userRepository,
            IDocumentRepository documentRepository
            )
        {
            _cloudinaryService = cloudinaryService;
            _userRepository = userRepository;
            _documentRepository = documentRepository;
        }

        public async Task<UploadDocumentResult> UploadDocument(UploadDocumentRequestDto data, TokenData jwtTokenData)
        {
            var user = await _userRepository.GetUserById(jwtTokenData.UserId);

            if (user == null)
            {
                return new UploadDocumentResult()
                {
                    Success = false,
                    Errors = new List<string>(){
                        "User does not exist."
                    }
                };
            }


            var uploadResult = await _cloudinaryService.UploadFile(data.File);

            if (uploadResult.StatusCode != HttpStatusCode.OK)
            {
                return new UploadDocumentResult()
                {
                    Success = false,
                    Errors = {
                        "Unexpected error during file uploading"
                    }
                };
            }

            var document = new Document()
            {
                Id = uploadResult.DocumentId,
                FileName = uploadResult.DocumentName,
                Description = data.Description,
                Owner = user,
                AllowedUsers = new List<User>()
            };

            await _documentRepository.CreateDocument(document);

            return new UploadDocumentResult()
            {
                Success = true,
                DocumentId = uploadResult.DocumentId,
                DocumentExtension = uploadResult.DocumentExtension,
                DocumentName = uploadResult.DocumentName
            };
        }
    }
}