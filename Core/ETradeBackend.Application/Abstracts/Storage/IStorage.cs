using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Application.Abstracts.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles);
        Task DeleteAsync(string path, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);

    }
}
