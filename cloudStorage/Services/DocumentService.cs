using System.Net;
using System.Text.Json;
using CloudStorage.Constants.ErrorMessages;
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
                        UploadDocumentErrorMessages.UserDoesNotExist
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
                        UploadDocumentErrorMessages.UnexpectedError
                    }
                };
            }

            var document = new Document()
            {
                Id = uploadResult.DocumentId,
                FileName = uploadResult.DocumentName,
                FileExtension = uploadResult.DocumentExtension,
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

        public async Task<List<Document>> GetAllDocumentsByOwnerId(string ownerId)
        {
            var documents = await _documentRepository.GetDocumentsByOwner(ownerId);

            return documents;
        }
    }
}