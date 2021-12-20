using Bogus;
using Moq;
using Repository.Interface;
using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using XUnitTest.Builder;

namespace XUnitTest.Server
{
    public class ServerServiceTest
    {
        private readonly ITestOutputHelper _output;
        public ServerServiceTest(ITestOutputHelper output)
        {
            _output = output;
            output.WriteLine("Construtor");
        }
        [Fact]
        public async void AddServerSuccess()
        {
            Mock<IServerRepository> mockServerRepository = new Mock<IServerRepository>();
            var serverIn = ServerInBuilder.New().Build();
            mockServerRepository.Setup(x => x.AddAsync(serverIn)).ReturnsAsync(true);
            IServerService serverService =
                new ServerService(mockServerRepository.Object);

            var success = await serverService.AddAsync(serverIn);

            mockServerRepository
                .Verify(x => x.AddAsync(serverIn), Times.Exactly(1));
            Assert.True(success);
        }

        [Theory]
        [InlineData("777.444.111.444")]
        [InlineData("77441144")]
        [InlineData("Adriano")]
        public void AddServerIncorrectIpFormat(string IncorrectIpFormat)
        {
            Mock<IServerRepository> mockServerRepository = new Mock<IServerRepository>();
            var serverIn = ServerInBuilder.New().WithIp(IncorrectIpFormat).Build();
            mockServerRepository.Setup(x => x.AddAsync(serverIn)).ReturnsAsync(true);
            IServerService serverService =
                new ServerService(mockServerRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => serverService.AddAsync(serverIn));
        }


        [Fact]
        public async void UpdateServerSuccess()
        {
            Faker faker = new Faker();
            var id = faker.Random.Guid().ToString();
            Mock<IServerRepository> mockServerRepository = new Mock<IServerRepository>();
            var serverIn = ServerInBuilder.New().Build();
            mockServerRepository.Setup(x => x.UpdateAsync(id, serverIn)).ReturnsAsync(true);
            IServerService serverService =
                new ServerService(mockServerRepository.Object);

            var success = await serverService.UpdateAsync(id, serverIn);

            mockServerRepository
                .Verify(x => x.UpdateAsync(id, serverIn), Times.Exactly(1));
            Assert.True(success);
        }

        [Theory]
        [InlineData("777.444.111.444")]
        [InlineData("77441144")]
        [InlineData("Adriano")]
        public void UpdateIncorrectIpFormat(string IncorrectIpFormat)
        {
            Faker faker = new Faker();
            var id = faker.Random.Guid().ToString();
            Mock<IServerRepository> mockServerRepository = new Mock<IServerRepository>();
            var serverIn = ServerInBuilder.New().WithIp(IncorrectIpFormat).Build();
            mockServerRepository.Setup(x => x.UpdateAsync(id, serverIn)).ReturnsAsync(true);
            IServerService serverService =
                new ServerService(mockServerRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => serverService.UpdateAsync(id, serverIn));

        }
    }
}
