using Microsoft.AspNetCore.Http;

namespace Service.Contracts;
public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file);
    Task<(Stream Stream, string FileName)?> GetFileAsync(string relativePath, string originalName);
}