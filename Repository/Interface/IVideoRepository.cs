using Model.In;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVideoRepository : IRepositoryBase<VideoIn, VideoOut>
    {
        List<VideoOut> GetAllByServer(string serverId);
        Task<VideoOut> GetModelByIdAsync(string serverId, string id);
        Task<bool> DeleteAsync(string id, string serverId);
        List<VideoOut> GetOldVideosByDate(DateTimeOffset Date);
        bool HardDeleteMultiThreaded(string id);
    }
}
