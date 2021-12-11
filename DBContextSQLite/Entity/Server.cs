using DBContextSQLite.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContextSQLite.Entity
{
    public class Server: EntityBase
    {
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }       
    }
}
