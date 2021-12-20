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
            return (await SaveChanges()) > 0;
        }
        public async Task<bool> UpdateAsync(string id, ModelIn modelId)
        {
            Entity entity = await GetById(id);
            entity.UpdateFrom(modelId);
            return (await SaveChanges()) > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Entity entity = await GetById(id);
            entity.Deleted = true;
            return (await SaveChanges()) > 0;
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
        public async Task<int> SaveChanges()
        {
            try
            {
                return await videoMonitoringContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Um comando de banco de dados não afetou o número esperado de linhas. Isso geralmente indica uma violação de simultaneidade otimista; ou seja, uma linha foi alterada no banco de dados desde que foi consultada.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocorreu um erro ao enviar atualizações para o banco de dados.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new Exception("Foi feita uma tentativa de usar um comportamento sem suporte, como a execução simultânea de vários comandos assíncronos na mesma instância de contexto.", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new Exception("O contexto ou a conexão foram descartados.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Ocorreu um erro ao tentar processar entidades no contexto antes ou depois de enviar comandos para o banco de dados.", ex);
            }
        }


    }
}
