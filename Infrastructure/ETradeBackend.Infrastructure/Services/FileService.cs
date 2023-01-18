using ETradeBackend.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;

namespace ETradeBackend.Infrastructure.Services
{
    public class FileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            var newFileName = await Task.Run<string>(async () =>
             {
                 string extension = Path.GetExtension(fileName);
                 string oldName = Path.GetFileNameWithoutExtension(fileName);
                 string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                 if (File.Exists($"{path}\\{newFileName}"))
                     return $"{NameOperation.CharacterRegulatory(oldName)}-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}{extension}";
                 return newFileName;
             });
            return newFileName;
        }

    }
}
