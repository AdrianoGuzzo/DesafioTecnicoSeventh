using System;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private const string NAME_SYSTEM = "Desafio Técnico Seventh";
        private static readonly string[] VERSIONS = { "v1" };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureServicesSwagger(services);
            Bootstrap.Start(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ConfigureSwagger(app);
        }
        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                foreach (var VERSION in VERSIONS)
                {
                    c.SwaggerDoc(VERSION, new OpenApiInfo
                    {
                        Title = NAME_SYSTEM,
                        Version = VERSION,
                        Description = NAME_SYSTEM,
                        Contact = new OpenApiContact
                        {
                            Name = "seventh",
                            Url = new Uri("http://www.seventh.com.br")
                        }
                    });
                }

                c.IncludeXmlComments(System.String.Format(@"{0}API.xml",
                System.AppDomain.CurrentDomain.BaseDirectory));
            });
        }
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var VERSION in VERSIONS)
                {
                    c.SwaggerEndpoint($"/swagger/{VERSION}/swagger.json",
                     $"{NAME_SYSTEM} {VERSION}");
                }
            });
        }
    }
}
