using System.Net;
using System.Text.Json;
using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Models;

namespace CloudStorage.Services
{
    public class DokumentService : IDokumentService
    {

        private ICloudinaryService _cloudinaryService;
        private IUserRespository _userRepository;
        private IDokumentRepository _dokumentRepository;

        public DokumentService(
            ICloudinaryService cloudinaryService,
            IUserRespository userRepository,
            IDokumentRepository dokumentRepository
            )
        {
            _cloudinaryService = cloudinaryService;
            _userRepository = userRepository;
            _dokumentRepository = dokumentRepository;
        }

        public async Task<UploadDokumentResult> UploadDokument(UploadDokumentRequestDto data, TokenData jwtTokenData)
        {
            var user = await _userRepository.GetUserById(jwtTokenData.UserId);

            if (user == null)
            {
                return new UploadDokumentResult()
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
                return new UploadDokumentResult()
                {
                    Success = false,
                    Errors = {
                        "Unexpected error during file uploading"
                    }
                };
            }

            var dokument = new Dokument()
            {
                Id = uploadResult.DokumentId,
                FileName = uploadResult.DokumentName,
                Description = data.Description,
                Owner = user,
                AllowedUsers = new List<User>()
            };

            Console.WriteLine("dokument: ");
            Console.WriteLine(JsonSerializer.Serialize(dokument));

            await _dokumentRepository.CreateDokumnet(dokument);

            return new UploadDokumentResult()
            {
                Success = true,
                DokumentId = uploadResult.DokumentId,
            };
        }
    }
}