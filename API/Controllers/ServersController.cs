using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class ServersController : ControllerBase
    {
        private readonly IServerService _serverService;
        public ServersController(IServerService serverService)
        => _serverService = serverService;

        /// <summary>
        /// Criar um novo servidor
        /// </summary>  
        [HttpPost]
        public async Task<bool> Post(ServerIn serverIn)
            => await _serverService.AddAsync(serverIn);

        /// <summary>
        /// Atualizar um servidor
        /// </summary>  
        [HttpPatch("{serverId}")]
        public async Task<bool> Patch(string serverId, ServerIn serverIn)
            => await _serverService.UpdateAsync(serverId, serverIn);

        /// <summary>
        /// Remover um servidor (mudado o estado do Deleted=true)
        /// </summary>  
        [HttpDelete("{serverId}")]
        public async Task<bool> Delete(string serverId)
            => await _serverService.DeleteAsync(serverId);

        /// <summary>
        /// Listar todos os servidores
        /// </summary>  
        [HttpGet()]
        public List<ServerOut> GetAll()
            => _serverService.GetAll();

        /// <summary>
        /// Recuperar um servidor
        /// </summary>  
        [HttpGet("{serverId}")]
        public async Task<ServerOut> Get(string serverId)
            => await _serverService.GetModelByIdAsync(serverId);

        /// <summary>
        /// Checar disponibilidade de um servidor
        /// </summary>  
        [HttpGet()]
        [Route("Available/{serverId}")]
        public async Task<bool> Available(string serverId)
            => await _serverService.AvailableAsync(serverId);
    }
}
