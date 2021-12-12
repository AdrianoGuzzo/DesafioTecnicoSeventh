using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IRepositoryBase<ModelIn, ModelOut>
    {
        bool Add(ModelIn modelId);
        bool Update(string id, ModelIn modelId);
        bool Delete(string id);   
        ModelOut GetModelById(string id);
    }
}
