using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepositoryBase<ModelIn, ModelOut>
    {
        Task<bool> AddAsync(ModelIn modelId);
        Task<bool> UpdateAsync(string id, ModelIn modelId);
        Task<bool> DeleteAsync(string id);
        Task<ModelOut> GetModelByIdAsync(string id);
    }
}
