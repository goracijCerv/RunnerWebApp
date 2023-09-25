using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunnerWebApp.Helpers;
using RunnerWebApp.Interfaces;

namespace RunnerWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary; 

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );

            _cloudinary = new Cloudinary( acc );
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photoFile)
        {
            var uploadRes = new ImageUploadResult();

            if (photoFile.Length > 0)
            {
                using var stream = photoFile.OpenReadStream();
                var uploadParm = new ImageUploadParams
                {
                    File = new FileDescription(photoFile.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadRes = await _cloudinary.UploadAsync(uploadParm);
            }
            return uploadRes;
        }

        public async Task<DeletionResult> DelateImageAsync(string publicId)
        {
            var deleteParms = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParms);
            return result;
        }
    }
}
