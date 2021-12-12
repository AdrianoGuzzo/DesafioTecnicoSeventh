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
        [HttpPatch("{id}")]
        public bool Patch(string id, ServerIn serverIn)
            => _serverService.Update(id, serverIn);
        /// <summary>
        /// Remover um servidor (mudado o estado do Deleted=true)
        /// </summary>  
        [HttpDelete("{id}")]
        public bool Delete(string id)
            => _serverService.Delete(id);

        /// <summary>
        /// Traz uma lista de servidores
        /// </summary>  
        [HttpGet()]
        [Route("GetAll")]
        public List<ServerOut> GetAll()
            => _serverService.GetAll();
        /// <summary>
        /// Recuperar um servidor
        /// </summary>  
        [HttpGet("{id}")]
        public ServerOut Get(string id)
            => _serverService.GetModelById(id);
    }
}
