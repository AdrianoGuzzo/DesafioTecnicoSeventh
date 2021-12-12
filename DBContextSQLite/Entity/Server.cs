using DBContextSQLite.Entity.Base;
using Model.In;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContextSQLite.Entity
{
    public class Server : EntityBase<ServerIn, ServerOut>
    {
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public override void CreateFrom(ServerIn modelIn)
        {
            this.Name = modelIn.Name;
            this.Ip = modelIn.Ip;
            this.Port = modelIn.Port;
        }
        public override void UpdateFrom(ServerIn modelIn)
        {
            this.Name = modelIn.Name;
            this.Ip = modelIn.Ip;
            this.Port = modelIn.Port;
        }

        public override ServerOut MapperToOut()
        => EntiyToOut(this);

        public static Func<Server, ServerOut> ProjectionToOut()
            => x => EntiyToOut(x);

        private static ServerOut EntiyToOut(Server entity)
            => new ServerOut(entity.Id, entity.Name, entity.Ip, entity.Port);

    }
}
