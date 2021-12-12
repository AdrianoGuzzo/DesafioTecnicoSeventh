using DBContextSQLite;
using DBContextSQLite.Entity;
using Model.In;
using Model.Out;
using Repository.Base;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class VideoRepository : RepositoryBase<VideoIn, VideoOut, Video>, IVideoRepository
    {
        public VideoRepository(VideoMonitoringContext videoMonitoringContext) : base(videoMonitoringContext)
        {
        }

        public VideoOut GetModelById(string serverId, string id)
        {
            var entity = GetVideo(id, serverId);
            return entity.MapperToOut();
        }
        public bool Delete(string id, string serverId)
        {
            var entity = GetVideo(id, serverId);
            entity.Deleted = true;
            return SaveChanges() > 0;
        }

        private Video GetVideo(string id, string serverId)
        {
            var entity = videoMonitoringContext.Video
                   .Where(x => x.Id == Guid.Parse(id) && x.ServerId == Guid.Parse(serverId) && !x.Deleted)
                   .FirstOrDefault();
            if (entity == null)
                throw new Exception("Vídeo não encontrado");
            return entity;
        }

        public List<VideoOut> GetAllByServer(string serverId)
        => videoMonitoringContext.Video
            .Where(x => x.ServerId == Guid.Parse(serverId) && !x.Deleted)
            .Select(Video.ProjectionToOut())
            .ToList();
    }
}
