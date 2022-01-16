using Bogus;
using Model.In;
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
        private readonly Mock<IServerRepository> _mockServerRepository;
        private readonly IServerService _serverService;
        private readonly ServerIn _serverIn;
        private readonly string _IdServer;

        public ServerServiceTest(ITestOutputHelper output)
        {
            Faker faker = new Faker();
            _serverIn = ServerInBuilder.New().Build();
            _IdServer = faker.Random.Guid().ToString();
            _mockServerRepository = new Mock<IServerRepository>();
            _serverService = new ServerService(_mockServerRepository.Object);
            _output = output;
            output.WriteLine("Construtor");
        }


        [Fact]
        public async void AddServerSuccess()
        {
            _mockServerRepository.Setup(x => x.AddAsync(_serverIn)).ReturnsAsync(true);

            var success = await _serverService.AddAsync(_serverIn);

            _mockServerRepository
                .Verify(x => x.AddAsync(_serverIn), Times.Exactly(1));
            Assert.True(success);
        }

        [Theory]
        [InlineData("777.444.111.444")]
        [InlineData("77441144")]
        [InlineData("Adriano")]
        public void AddServerIncorrectIpFormat(string IncorrectIpFormat)
        {
            var serverIn = ServerInBuilder.New().WithIp(IncorrectIpFormat).Build();
            _mockServerRepository.Setup(x => x.AddAsync(serverIn)).ReturnsAsync(true);

            Assert.ThrowsAsync<ArgumentException>(() => _serverService.AddAsync(serverIn));
        }


        [Fact]
        public async void UpdateServerSuccess()
        {
            Faker faker = new Faker();      

            _mockServerRepository.Setup(x => x.UpdateAsync(_IdServer, _serverIn)).ReturnsAsync(true);

            var success = await _serverService.UpdateAsync(_IdServer, _serverIn);

            _mockServerRepository
                .Verify(x => x.UpdateAsync(_IdServer, _serverIn), Times.Exactly(1));
            Assert.True(success);
        }

        [Theory]
        [InlineData("777.444.111.444")]
        [InlineData("77441144")]
        [InlineData("Adriano")]
        public void UpdateIncorrectIpFormat(string IncorrectIpFormat)
        {              
            var serverIn = ServerInBuilder.New().WithIp(IncorrectIpFormat).Build();
            _mockServerRepository.Setup(x => x.UpdateAsync(_IdServer, serverIn)).ReturnsAsync(true);

            Assert.ThrowsAsync<ArgumentException>(() => _serverService.UpdateAsync(_IdServer, serverIn));
        }
    }
}
