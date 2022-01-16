using Bogus;
using Model.In;
using Model.Out;
using Moq;
using Repository.Interface;
using Service;
using Service.Interface;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTest.Video
{
    public class VideoServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly Mock<IVideoRepository> _mockVideoRepository ;
        private readonly Mock<IFileRepository> _mockFileRepository;
        public VideoServiceTest(ITestOutputHelper output)
        {
            _mockVideoRepository = new Mock<IVideoRepository>();
            _mockFileRepository = new Mock<IFileRepository>();
            _output = output;
            output.WriteLine("Construtor");
        }

        [Fact]
        public async void AddVideoSuccess()
        {
            Faker faker = new Faker();
            var base64 = faker.Name.Random.String(1000);      

            _mockVideoRepository.Setup(x => x.AddAsync(It.IsAny<VideoIn>())).ReturnsAsync(true);
            _mockFileRepository.Setup(x => x.SaveFileAsync(base64)).ReturnsAsync(new FileOut(faker.Random.Guid(), faker.Random.Int(min: 0)));
            IVideoService videoService =
                new VideoService(_mockVideoRepository.Object, _mockFileRepository.Object);

            var success = await videoService.AddAsync(faker.Random.Guid().ToString(), faker.Name.Random.Words(10), base64);

            Assert.True(success);
        }
    }
}
