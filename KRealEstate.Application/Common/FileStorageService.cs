
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace KRealEstate.Application.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private readonly string USER_CONTENT_FOLDER_NAME = "user-content";
        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }
        public Task DeleteFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public string getFileUrl(string fileName, string childPath)
        {
            if (childPath == null)
            {
                return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
            }
            return $"/{USER_CONTENT_FOLDER_NAME}/{childPath}/{fileName}";
        }

        public async Task<string> SaveFile(IFormFile file, string childPath)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFileAsync(file.OpenReadStream(), fileName, childPath);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + childPath + "/" + fileName;
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName, string childPath)
        {
            var filePath = Path.Combine(_userContentFolder + "/" + childPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}
