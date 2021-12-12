using Model.In;
using Model.Out;
using Repository.Interface;
using Service.Base;
using Service.Interface;
using System;
using System.Collections.Generic;
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
    }
}
