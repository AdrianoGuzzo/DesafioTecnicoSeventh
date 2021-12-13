using Bogus;
using Model.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.Builder
{
    public class ServerInBuilder
    {

        private string Name { get; set; }
        private string Ip { get; set; }
        private int Port { get; set; }
        public ServerInBuilder()
        {
            Faker faker = new Faker();
            Name = faker.Name.Random.Words(3);
            Ip = faker.Internet.Ip();
            Port = faker.Internet.Port();
        }
        public static ServerInBuilder New()
        => new ServerInBuilder();

        public ServerInBuilder WithIp(string ip)
        {
            Ip = ip;
            return this;
        }
        public ServerIn Build()
           => new ServerIn
           {
               Ip = Ip,
               Port = Port,
               Name = Name,
           };
    }
}
