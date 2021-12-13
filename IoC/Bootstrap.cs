using DBContextSQLite;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;

namespace IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services)
        {
            services.AddDbContext<VideoMonitoringContext>();

            StartServices(services);
            StartRepositories(services);
        }
        private static void StartServices(IServiceCollection services)
        {
            services
                .AddScoped<IServerService, ServerService>()
                .AddScoped<IVideoService, VideoService>()
                .AddScoped<IFileRepository, FileRepository>();
        }
        private static void StartRepositories(IServiceCollection services)
        {
            services
                .AddScoped<IServerRepository, ServerRepository>()
                .AddScoped<IVideoRepository, VideoRepository>();
        }
    }
}
