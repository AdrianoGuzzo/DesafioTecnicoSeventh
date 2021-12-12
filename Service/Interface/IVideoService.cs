using Model.In;
using Model.Out;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IVideoService : IServiceModelBase<VideoIn, VideoOut>
    {
        bool Add(string serverId, string description, string FileInBase64);
        List<VideoOut> GetAllByServer(string serverId);
        VideoOut GetModelById( string serverId, string id);        
        bool Delete(string id, string serverId);
        byte[] GetBinary(string serverId, string Id);
        bool RecyclerProcess(int days);
        RecyclerStatusOut GetRecyclerStatus();
    }
}
