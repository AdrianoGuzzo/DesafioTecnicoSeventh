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
        public void AddVideoSuccess()
        {
            Faker faker = new Faker();
            var base64 = faker.Name.Random.String(1000);
            Mock<IVideoRepository> mockVideoRepository = new Mock<IVideoRepository>();
            Mock<IFileRepository> mockFileRepository = new Mock<IFileRepository>();
            mockVideoRepository.Setup(x => x.Add(It.IsAny<VideoIn>())).Returns(true);
            mockFileRepository.Setup(x => x.SaveFile(base64)).Returns(new FileOut(faker.Random.Guid(), faker.Random.Int(min: 0)));
            IVideoService videoService =
                new VideoService(mockVideoRepository.Object, mockFileRepository.Object);

            var success = videoService.Add(faker.Random.Guid().ToString(), faker.Name.Random.Words(10), base64);

            Assert.True(success);
        }
    }
}
