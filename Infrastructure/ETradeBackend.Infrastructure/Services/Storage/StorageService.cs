using ETradeBackend.Application.Abstracts.Storage;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string path, string fileName)
        => await _storage.DeleteAsync(path, fileName);

        public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles)
        => await _storage.UploadAsync(pathOrContainerName, formFiles);
    }
}
