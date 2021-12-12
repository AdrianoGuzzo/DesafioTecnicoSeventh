using Model.In;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IVideoRepository : IRepositoryBase<VideoIn, VideoOut>
    {
        List<VideoOut> GetAllByServer(string serverId);
        VideoOut GetModelById(string serverId, string id);
        bool Delete(string id, string serverId);
    }
}
