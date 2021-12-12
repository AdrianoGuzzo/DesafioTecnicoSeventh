using Model.In;
using Model.Out;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IServerService : IServiceModelBase<ServerIn, ServerOut>
    {
        List<ServerOut> GetAll();
        bool Available(string id);
    }
}
