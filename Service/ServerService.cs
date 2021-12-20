using Model.In;
using Model.Out;
using Repository.Interface;
using Service.Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Service
{
    public class ServerService : ServiceModelBase<ServerIn, ServerOut>, IServerService
    {
        private readonly IServerRepository _serverRepository;
        public ServerService(IServerRepository repository) : base(repository)
        {
            _serverRepository = repository;
        }

        public List<ServerOut> GetAll()
            => _serverRepository.GetAll();


        public async Task<bool> AvailableAsync(string serverId)
        {
            var server = await _serverRepository.GetModelByIdAsync(serverId);
            return await PingHostAsync(server.Ip, server.Port);
        }

        public async Task<bool> PingHostAsync(string ip, int port)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                var tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ipAddress, port);
                //using (var client = new TcpClient(ipAddress, portNumber))
                return tcpClient.Connected;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar connectar ao servidor", ex);
            }
        }

    }
}
