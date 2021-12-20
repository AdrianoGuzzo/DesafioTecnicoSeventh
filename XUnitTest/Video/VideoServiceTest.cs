using Bogus;
using Model.In;
using Model.Out;
using Moq;
using Repository.Interface;
using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTest.Video
{
    public class VideoServiceTest
    {
        private readonly ITestOutputHelper _output;
        public VideoServiceTest(ITestOutputHelper output)
        {
            _output = output;
            output.WriteLine("Construtor");
        }

        [Fact]
        public async void AddVideoSuccess()
        {
            Faker faker = new Faker();
            var base64 = faker.Name.Random.String(1000);
            Mock<IVideoRepository> mockVideoRepository = new Mock<IVideoRepository>();
            Mock<IFileRepository> mockFileRepository = new Mock<IFileRepository>();
            mockVideoRepository.Setup(x => x.AddAsync(It.IsAny<VideoIn>())).ReturnsAsync(true);
            mockFileRepository.Setup(x => x.SaveFileAsync(base64)).ReturnsAsync(new FileOut(faker.Random.Guid(), faker.Random.Int(min: 0)));
            IVideoService videoService =
                new VideoService(mockVideoRepository.Object, mockFileRepository.Object);

            var success = await videoService.AddAsync(faker.Random.Guid().ToString(), faker.Name.Random.Words(10), base64);

            Assert.True(success);
        }
    }
}
