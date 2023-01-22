using ETradeBackend.Infrastructure.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainer, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainer, string fileName, HasFile hasFileMethod)
        {
            var newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                if (hasFileMethod(pathOrContainer, newFileName))
                    return $"{NameOperation.CharacterRegulatory(oldName)}-{DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss")}{extension}";
                return newFileName;
            });
            return newFileName;
        }
    }
}
