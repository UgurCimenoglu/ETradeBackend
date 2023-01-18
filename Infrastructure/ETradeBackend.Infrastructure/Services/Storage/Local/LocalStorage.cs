using ETradeBackend.Application.Abstracts.Storage.LocalStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Infrastructure.Services.Storage.LocalStorage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)

           => File.Delete($"{path}\\{fileName}");


        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(x => x.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in formFiles)
            {
                var result = await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{path}\\{file.Name}"));
            }

            //todo custom exception fırlatılacak.
            return datas;
        }
        private async Task<bool> CopyFileAsync(string path, IFormFile file)
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
    }
}
