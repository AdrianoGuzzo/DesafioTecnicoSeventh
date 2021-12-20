using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBContextSQLite.Interface
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
