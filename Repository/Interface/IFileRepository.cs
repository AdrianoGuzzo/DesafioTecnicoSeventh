using Model.Out;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IFileRepository

    {
        Task<FileOut> SaveFileAsync(string base64);
        void RemoveFile(string fileName);
        Task<string> GetFileInBase64Async(string fileName);
        Task<byte[]> GetBinaryAsync(string fileName);
    }
}
