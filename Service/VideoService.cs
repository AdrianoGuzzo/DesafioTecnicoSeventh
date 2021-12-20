using Model.In;
using Model.Out;
using Repository.Interface;
using Service.Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class VideoService : ServiceModelBase<VideoIn, VideoOut>, IVideoService
    {
        private readonly IFileRepository _filerRepository;
        private readonly IVideoRepository _videoRepository;
        private static Task<bool> TasksRecycler = null;
        public VideoService(IVideoRepository repository, IFileRepository filerRepository) : base(repository)
        {
            _filerRepository = filerRepository;
            _videoRepository = repository;
        }
        public async Task<bool> AddAsync(string serverId, string description, string fileInBase64)
        {
            var videoIn = new VideoIn(serverId, description);
            var fileOut = await _filerRepository.SaveFileAsync(fileInBase64);
            videoIn.SetFileInfo(fileOut);
            try
            {
                videoIn.ValidationModel();
                return await _videoRepository.AddAsync(videoIn);
            }
            catch (Exception ex)
            {
                _filerRepository.RemoveFile(fileOut.Id.ToString());
                throw ex;
            }
        }
        public async Task<bool> DeleteAsync(string id, string serverId)
        => await _videoRepository.DeleteAsync(id, serverId);

        public List<VideoOut> GetAllByServer(string serverId)
         => _videoRepository.GetAllByServer(serverId);

        public async Task<byte[]> GetBinaryAsync(string serverId, string Id)
        {
            var videoOut = await _videoRepository.GetModelByIdAsync(serverId, Id);
            return await _filerRepository.GetBinaryAsync(videoOut.Id.ToString());
        }

        public async Task<VideoOut> GetModelByIdAsync(string serverId, string id)
            => await _videoRepository.GetModelByIdAsync(serverId, id);

        public bool RecyclerProcess(int days)
        {
            if (TasksRecycler == null || TasksRecycler.IsCompleted)
            {
                var deadline = DateTime.UtcNow.AddDays(-days);
                var videos = _videoRepository.GetOldVideosByDate(deadline);
                TasksRecycler = Task.Run(() =>
                {
                    foreach (var video in videos)
                    {
                        _filerRepository.RemoveFile(video.Id.ToString());
                        _videoRepository.HardDeleteMultiThreaded(video.Id.ToString());
                    }
                    return true;
                });
                return true;
            }
            return false;
        }

        public RecyclerStatusOut GetRecyclerStatus()
        {
            return new RecyclerStatusOut
            {
                Status = TasksRecycler == null || TasksRecycler.IsCompleted
                ? "not running"
                : "running"
            };
        }
    }
}
