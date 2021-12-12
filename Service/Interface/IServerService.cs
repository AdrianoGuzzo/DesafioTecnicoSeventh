using Model.In;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IServerService: IServiceModelBase<ServerIn, ServerOut>
    {
        List<ServerOut> GetAll();
    }
}
