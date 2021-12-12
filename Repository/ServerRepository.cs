using DBContextSQLite;
using DBContextSQLite.Entity;
using Model.In;
using Model.Out;
using Repository.Base;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ServerRepository : RepositoryBase<ServerIn, ServerOut, Server>, IServerRepository
    {
        public ServerRepository(VideoMonitoringContext videoMonitoringContext) : base(videoMonitoringContext)
        {
        }

        public List<ServerOut> GetAll()
            => videoMonitoringContext.Server
            .Where(x => !x.Deleted)
            .Select(Server.ProjectionToOut())
            .ToList();
    }
}
