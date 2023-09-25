using CloudinaryDotNet.Actions;

namespace RunnerWebApp.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DelateImageAsync(string publicid);
    }
}
