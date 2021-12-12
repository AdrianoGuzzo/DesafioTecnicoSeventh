using Model.Out;

namespace Repository.Interface
{
    public interface IFilerRepository
    {
        FileOut SaveFile(string base64);
        void RemoveFile(string fileName);
        string GetFileInBase64(string fileName);
        byte[] GetBinary(string fileName);
    }
}
