using DBContextSQLite;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services)
        {
            services.AddDbContext<VideoMonitoringContext>();
            //services.AddSingleton<INotificationFirebase, NotificationFirebase>();

            //StartServices(services);
            //StartRepositories(services);
        }
        private static void StartServices(IServiceCollection services)
        {
            //services
            //    .AddScoped<IUserService, UserService>()
            //    .AddScoped<INotificationFirebaseService, NotificationFirebaseService>();
        }
        private static void StartRepositories(IServiceCollection services)
        {
            //services
            //    .AddScoped<IUserRepository, UserRepository>()
            //    .AddScoped<ITokenFirebaseRepository, TokenFirebaseRepository>();
        }
    }
}
