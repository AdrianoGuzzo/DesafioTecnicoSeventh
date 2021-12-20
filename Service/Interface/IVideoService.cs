using Model.In;
using Model.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IVideoService : IServiceModelBase<VideoIn, VideoOut>
    {
        Task<bool> AddAsync(string serverId, string description, string fileInBase64);
        List<VideoOut> GetAllByServer(string serverId);
        Task<VideoOut> GetModelByIdAsync(string serverId, string id);
        Task<bool> DeleteAsync(string id, string serverId);
        Task<byte[]> GetBinaryAsync(string serverId, string Id);
        bool RecyclerProcess(int days);
        RecyclerStatusOut GetRecyclerStatus();
    }
}
