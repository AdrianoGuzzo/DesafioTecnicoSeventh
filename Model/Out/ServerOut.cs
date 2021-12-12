using System;

namespace Model.Out
{
    public class ServerOut
    {
        public ServerOut(Guid id, string name, string ip, int port)
        {
            Id = id;
            Name = name;
            Ip = ip;
            Port = port;
        }
        public Guid Id { get; }
        public string Name { get; }
        public string Ip { get; }
        public int Port { get; }
    }
}
