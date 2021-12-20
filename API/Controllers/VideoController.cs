using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Out;
using Service.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService)

        => _videoService = videoService;

        /// <summary>
        /// Adicionar um novo vídeo à um servidor
        /// </summary>  
        [HttpPost]
        [Route("/api/v{version:apiVersion}/servers/{serverId}/videos")]
        public async Task<bool> Post(string serverId, VideoViewModel videoViewModel)
            => await _videoService.AddAsync(serverId, videoViewModel.Description, videoViewModel.FileInBase64);

        /// <summary>
        /// Remover um vídeo existente (mudado o estado do Deleted=true)
        /// </summary>  
        [HttpDelete("/api/v{version:apiVersion}/servers/{serverId}/videos/{videoId}")]
        public async Task<bool> Delete(string serverId, string videoId)
            => await _videoService.DeleteAsync(videoId);

        /// <summary>
        /// Recuperar dados cadastrais de um vídeo
        /// </summary>  
        [HttpGet]
        [Route("/api/v{version:apiVersion}/servers/{serverId}/videos/{videoId}")]
        public async Task<VideoOut> Get(string serverId, string videoId)
            => await _videoService.GetModelByIdAsync(serverId, videoId);

        /// <summary>
        /// Listar todos os vídeos de um servidor 
        /// </summary>  
        [HttpGet]
        [Route("/api/v{version:apiVersion}/servers/{serverId}/videos")]
        public List<VideoOut> GetAllByServer(string serverId)
            => _videoService.GetAllByServer(serverId);
        /// <summary>
        /// Listar todos os vídeos de um servidor 
        /// </summary> 
        [HttpGet]
        [Route("/api/v{version:apiVersion}/servers/{serverId}/videos/{videoId}/binary")]
        public async Task<byte[]> GetBinary(string serverId, string videoId)
        => await _videoService.GetBinaryAsync(serverId, videoId);

        [HttpPost]
        [Route("/api/v{version:apiVersion}/recycler/process/{days}")]
        public bool RecyclerProcess(int days)
        {
            var result = _videoService.RecyclerProcess(days);
            this.HttpContext.Response.StatusCode = result ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NotAcceptable; // I'm a teapot
            return result;
        }

        [HttpGet]
        [Route("/api/v{version:apiVersion}/recycler/status")]
        public RecyclerStatusOut GetRecyclerStatus()
        => _videoService.GetRecyclerStatus();

    }
    public class VideoViewModel
    {

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public string Description { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public string FileInBase64 { get; set; }
    }
}
