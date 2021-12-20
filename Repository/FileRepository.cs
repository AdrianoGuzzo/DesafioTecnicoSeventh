using Microsoft.Extensions.Configuration;
using Model.Out;
using Repository.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly string path;

        public FileRepository(IConfiguration configuration)
        {
            path = configuration.GetSection("Repository").GetSection("PathFile").Value;
        }

        public void RemoveFile(string fileName)
        {
            var fullPath = Path.GetFullPath(path);
            File.Delete(fullPath + fileName);
        }

        public async Task<FileOut> SaveFileAsync(string base64)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);
                var file = Convert.FromBase64String(base64);
                var idFile = Guid.NewGuid();
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
   
                using (FileStream stream = File.Open($@"{fullPath}{idFile}", FileMode.OpenOrCreate))
                {                 
                    await stream.WriteAsync(file,0,file.Length);
                }

                var fileInfo = new FileInfo(fullPath + idFile);
                long length = fileInfo.Length;
                return new FileOut(idFile, length);
            }
            catch (Exception ex)
            {
                return new FileOut(ex);
            }
        }

        public async Task<string> GetFileInBase64Async(string fileName)
        {
            Byte[] bytes = await GetBinaryAsync(fileName);
            String file = Convert.ToBase64String(bytes);
            return file;
        }
        public async Task<byte[]> GetBinaryAsync(string fileName)
        {
            var fullPath = Path.GetFullPath(path);           

            byte[] result;
            using (FileStream stream = File.Open(fullPath + fileName, FileMode.Open))
            {
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
            }

            return result;
        }

    }
}
