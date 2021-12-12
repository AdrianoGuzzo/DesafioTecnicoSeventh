using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBContextSQLite.Entity.Base
{
    public abstract class EntityBase<ModelIn, ModelOut>
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool Ativo { get; set; }
        public bool Deleted { get; set; }
        public abstract void CreateFrom(ModelIn modelIn);
        public abstract void UpdateFrom(ModelIn modelIn);
        public abstract ModelOut MapperToOut();        
    }
}
