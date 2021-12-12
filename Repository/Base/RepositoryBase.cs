using DBContextSQLite;
using DBContextSQLite.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Linq;

namespace Repository.Base
{
    public abstract class RepositoryBase<ModelIn, ModelOut, Entity> : IRepositoryBase<ModelIn, ModelOut> where Entity : EntityBase<ModelIn, ModelOut>
    {
        public readonly VideoMonitoringContext videoMonitoringContext;
        public RepositoryBase(VideoMonitoringContext videoMonitoringContext)
        {
            this.videoMonitoringContext = videoMonitoringContext;
        }
        public bool Add(ModelIn modelId)
        {
            Entity entity = CreateFrom(modelId);
            videoMonitoringContext.Add(entity);
            return SaveChanges() > 0;
        }
        public bool Update(string id, ModelIn modelId)
        {
            Entity entity = GetById(id);     
            entity.UpdateFrom(modelId);
            return SaveChanges() > 0;
        }

        public bool Delete(string id)
        {
            Entity entity = GetById(id);
            entity.Deleted = true;
            return SaveChanges() > 0;
        }        

        public ModelOut GetModelById(string id)
        {
            Entity entity = GetById(id);
            return entity.MapperToOut();
        }

        private Entity GetById(string id)
        {
            Entity entity = videoMonitoringContext.Set<Entity>()
                .SingleOrDefault(x => x.Id == Guid.Parse(id) && !x.Deleted);
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
        public int SaveChanges()
        {
            try
            {
                return videoMonitoringContext.SaveChanges();
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
