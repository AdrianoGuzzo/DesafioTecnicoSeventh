using Microsoft.Extensions.Configuration;
using Model.Out;
using Repository.Interface;
using System;
using System.IO;

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

        public FileOut SaveFile(string base64)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);
                var file = Convert.FromBase64String(base64);
                var idFile = Guid.NewGuid();
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                File.WriteAllBytes($@"{fullPath}{idFile}", file);

                var fileInfo = new FileInfo(fullPath + idFile);
                long length = fileInfo.Length;
                return new FileOut(idFile, length);
            }
            catch (Exception ex)
            {
                return new FileOut(ex);
            }
        }

        public string GetFileInBase64(string fileName)
        {
            Byte[] bytes = GetBinary(fileName);
            String file = Convert.ToBase64String(bytes);
            return file;
        }
        public byte[] GetBinary(string fileName)
        {
            var fullPath = Path.GetFullPath(path);
            byte[] bytes = File.ReadAllBytes(fullPath + fileName);

            return bytes;
        }

    }
}
