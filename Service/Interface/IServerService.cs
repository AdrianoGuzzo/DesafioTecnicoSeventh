using Model.In;
using Model.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IServerService : IServiceModelBase<ServerIn, ServerOut>
    {
        List<ServerOut> GetAll();
        Task<bool> AvailableAsync(string serverId);
    }
}
