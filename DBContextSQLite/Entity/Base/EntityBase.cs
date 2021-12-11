using System;
using System.Collections.Generic;
using System.Text;

namespace DBContextSQLite.Entity.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Ativo { get; set; }
        public bool Deleted { get; set; }
    }
}
