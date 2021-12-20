using DBContextSQLite;
using DBContextSQLite.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Threading.Tasks;

namespace Repository.Base
{
    public abstract class RepositoryBase<ModelIn, ModelOut, Entity> : IRepositoryBase<ModelIn, ModelOut> where Entity : EntityBase<ModelIn, ModelOut>
    {
        public readonly VideoMonitoringContext videoMonitoringContext;
        public RepositoryBase(VideoMonitoringContext videoMonitoringContext)
        {
            this.videoMonitoringContext = videoMonitoringContext;
        }
        public async Task<bool> AddAsync(ModelIn modelId)
        {
            Entity entity = CreateFrom(modelId);
            await videoMonitoringContext.AddAsync(entity);
            return true;
        }
        public async Task<bool> UpdateAsync(string id, ModelIn modelId)
        {
            Entity entity = await GetById(id);
            entity.UpdateFrom(modelId);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Entity entity = await GetById(id);
            entity.Deleted = true;
            return true;
        }

        public async Task<ModelOut> GetModelByIdAsync(string id)
        {
            Entity entity = await GetById(id);
            return entity.MapperToOut();
        }

        private async Task<Entity> GetById(string id)
        {
            Entity entity = await videoMonitoringContext.Set<Entity>()
                .SingleOrDefaultAsync(x => x.Id == Guid.Parse(id) && !x.Deleted);
            if (entity == null)
                throw new Exception("Entidade não encontrada");
            return entity;
        }

        private Entity CreateFrom(ModelIn modelOut)
        {
            var entity = (Entity)Activator.CreateInstance(typeof(Entity));
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreateFrom(modelOut);
            return entity;
        }       
    }
}
