using Model.In;
using Model.Out;
using Repository.Interface;
using Service.Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

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


        public bool Available(string serverId)
        {
            var server = _serverRepository.GetModelById(serverId);
            return PingHost(server.Ip, server.Port);
        }

        public bool PingHost(string ip, int port)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                var tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);
                //using (var client = new TcpClient(ipAddress, portNumber))
                return tcpClient.Connected;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar connectar no servidor", ex);
            }         
        }

    }
}
