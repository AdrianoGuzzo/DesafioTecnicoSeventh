using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Base
{
    public class ServiceModelBase<ModelIn, ModelOut> : IRepositoryBase<ModelIn, ModelOut>
    {
        public readonly IRepositoryBase<ModelIn, ModelOut> repository;
        public ServiceModelBase(IRepositoryBase<ModelIn, ModelOut> repository)
        {
            this.repository = repository;
        }
        public bool Add(ModelIn modelId)
        {
            return repository.Add(modelId);
        }
        public bool Update(string id, ModelIn modelId)
        {
            return repository.Update(id, modelId);
        }

        public bool Delete(string id)
        {
            return repository.Delete(id);
        }

        public ModelOut GetModelById(string id)
        => repository.GetModelById(id);     
    }
}
