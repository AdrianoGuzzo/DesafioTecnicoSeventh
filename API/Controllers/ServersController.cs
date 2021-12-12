using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        {
            _serverService = serverService;
        }
        /// <summary>
        /// Criar um novo servidor
        /// </summary>  
        [HttpPost]
        public bool Post(ServerIn serverIn)
            => _serverService.Add(serverIn);
        /// <summary>
        /// Atualizar um servidor
        /// </summary>  
        [HttpPatch("{serverId}")]
        public bool Patch(string serverId, ServerIn serverIn)
            => _serverService.Update(serverId, serverIn);
        /// <summary>
        /// Remover um servidor (mudado o estado do Deleted=true)
        /// </summary>  
        [HttpDelete("{serverId}")]
        public bool Delete(string serverId)
            => _serverService.Delete(serverId);

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
        public ServerOut Get(string serverId)
            => _serverService.GetModelById(serverId);

        /// <summary>
        /// Checar disponibilidade de um servidor
        /// </summary>  
        [HttpGet()]
        [Route("Available/{serverId}")]
        public bool Available(string serverId)
            => _serverService.Available(serverId);
    }
}
