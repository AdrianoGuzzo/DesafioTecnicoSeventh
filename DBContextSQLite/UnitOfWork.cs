using DBContextSQLite.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBContextSQLite
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly VideoMonitoringContext _videoMonitoringContext;
        public UnitOfWork(VideoMonitoringContext videoMonitoringContext)
        {
            _videoMonitoringContext = videoMonitoringContext;
        }

        public async Task Commit() {

            try
            {
                await _videoMonitoringContext.SaveChangesAsync();
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
