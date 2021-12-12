using Model.In;
using Model.Out;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IServerRepository: IRepositoryBase<ServerIn, ServerOut>
    {
        List<ServerOut> GetAll();
    }
}
