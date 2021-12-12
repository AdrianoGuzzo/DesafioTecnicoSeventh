using DBContextSQLite.Entity;
using Model.In;
using Model.Out;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IServerRepository: IRepositoryBase<ServerIn, ServerOut>
    {
        List<ServerOut> GetAll();
    }
}
