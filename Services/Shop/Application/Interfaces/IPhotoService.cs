using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

    Task<DeletionResult> DeletePhotoAsync(string publicId);
}