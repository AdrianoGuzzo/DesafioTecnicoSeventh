using DBContextSQLite;
using DBContextSQLite.Entity;
using Microsoft.Extensions.Configuration;
using Model.In;
using Model.Out;
using Repository.Base;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class VideoRepository : RepositoryBase<VideoIn, VideoOut, Video>, IVideoRepository
    {
        private readonly IConfiguration _configuration;

        public VideoRepository(VideoMonitoringContext videoMonitoringContext, IConfiguration configuration) : base(videoMonitoringContext)
        {
            _configuration = configuration;
        }

        public async Task<VideoOut> GetModelByIdAsync(string serverId, string id)
        {
            var entity = await GetVideo(id, serverId);
            return entity.MapperToOut();
        }
        public async Task<bool> DeleteAsync(string id, string serverId)
        {
            var entity = await GetVideo(id, serverId);
            entity.Deleted = true;
            return (await SaveChanges()) > 0;
        }

        private async Task<Video> GetVideo(string id, string serverId)
        {
            var entity = await videoMonitoringContext.Video
                   .Where(x => x.Id == Guid.Parse(id) && x.ServerId == Guid.Parse(serverId) && !x.Deleted)
                   .FirstOrDefaultAsync();
            if (entity == null)
                throw new Exception("Vídeo não encontrado");
            return entity;
        }

        public List<VideoOut> GetAllByServer(string serverId)
        => videoMonitoringContext.Video
            .Where(x => x.ServerId == Guid.Parse(serverId) && !x.Deleted)
            .Select(Video.ProjectionToOut())
            .ToList();

        public List<VideoOut> GetOldVideosByDate(DateTimeOffset Date)
        => videoMonitoringContext
            .Video.ToList().Where(x => x.CreatedAt < Date)
            .Select(Video.ProjectionToOut())
            .ToList();

        public bool HardDeleteMultiThreaded(string id)
        {
            using (VideoMonitoringContext videoMonitoringContext = new VideoMonitoringContext(_configuration))
            {
                var entity = videoMonitoringContext.Video.SingleOrDefault(x => x.Id == Guid.Parse(id));
                videoMonitoringContext.Video.Remove(entity);
                return videoMonitoringContext.SaveChanges() > 0;
            }

        }
    }
}
