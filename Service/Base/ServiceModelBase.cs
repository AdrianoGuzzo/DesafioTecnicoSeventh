using Model.In.Base;
using Repository.Interface;
using System.Threading.Tasks;

namespace Service.Base
{
    public class ServiceModelBase<ModelIn, ModelOut> : IRepositoryBase<ModelIn, ModelOut> where ModelIn : BaseIn
    {
        public readonly IRepositoryBase<ModelIn, ModelOut> repository;
        public ServiceModelBase(IRepositoryBase<ModelIn, ModelOut> repository)
        {
            this.repository = repository;
        }
        public virtual async Task<bool> AddAsync(ModelIn modelId)
        {
            modelId.ValidationModel();
            return await repository.AddAsync(modelId);
        }
        public virtual async Task<bool> UpdateAsync(string id, ModelIn modelId)
        {
            modelId.ValidationModel();
            return await repository.UpdateAsync(id, modelId);
        }

        public virtual async Task<bool> DeleteAsync(string id)
        => await repository.DeleteAsync(id);

        public virtual async Task<ModelOut> GetModelByIdAsync(string id)
        => await repository.GetModelByIdAsync(id);


    }
}
