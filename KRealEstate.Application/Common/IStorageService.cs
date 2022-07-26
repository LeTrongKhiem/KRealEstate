using Microsoft.AspNetCore.Http;

namespace KRealEstate.Application.Common
{
    public interface IStorageService
    {
        string getFileUrl(string fileName, string childPath);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName, string childPath);
        Task<string> SaveFile(IFormFile file, string childPath);
        Task DeleteFileAsync(string fileName);
    }
}
