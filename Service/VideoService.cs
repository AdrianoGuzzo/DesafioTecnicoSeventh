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
        public bool Add(string serverId, string description, string fileInBase64)
        {
            var videoIn = new VideoIn(serverId, description);
            var fileOut = _filerRepository.SaveFile(fileInBase64);
            videoIn.SetFileInfo(fileOut);
            try
            {
                videoIn.ValidationModel();
                return _videoRepository.Add(videoIn);
            }
            catch (Exception ex)
            {
                _filerRepository.RemoveFile(fileOut.Id.ToString());
                throw ex;
            }
        }
        public bool Delete(string id, string serverId)
        => _videoRepository.Delete(id, serverId);

        public List<VideoOut> GetAllByServer(string serverId)
         => _videoRepository.GetAllByServer(serverId);

        public byte[] GetBinary(string serverId, string Id)
        {
            var videoOut = _videoRepository.GetModelById(serverId, Id);
            return _filerRepository.GetBinary(videoOut.Id.ToString());
        }

        public VideoOut GetModelById(string serverId, string id)
            => _videoRepository.GetModelById(serverId, id);

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
