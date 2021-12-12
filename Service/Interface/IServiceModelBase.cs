using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IServiceModelBase<ModelIn, ModelOut> : IRepositoryBase<ModelIn, ModelOut>
    {
    }
}
