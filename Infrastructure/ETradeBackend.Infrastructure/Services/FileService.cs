using ETradeBackend.Application.Services;
using ETradeBackend.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in formFiles)
            {
                string newFileName = await FileRenameAsync(uploadPath, file.FileName);
                var result = await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
                datas.Add((newFileName, $"{path}\\{newFileName}"));
                results.Add(result);
            }

            if (results.TrueForAll(x => x.Equals(true)))
                return datas;

            //todo custom exception fırlatılacak.
            return null;

        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fs = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
