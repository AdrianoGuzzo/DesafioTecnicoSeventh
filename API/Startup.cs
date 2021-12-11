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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
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
                //exemplo de V2
                //c.SwaggerDoc("v2", new OpenApiInfo
                //{
                //    Title = "Desafio Técnico Seventh",
                //    Version = "v2",
                //    Description = "Desafio Técnico Seventhr",
                //    Contact = new OpenApiContact
                //    {
                //        Name = "seventh",
                //        Url = new Uri("http://www.seventh.com.br")
                //    }
                //});
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio Técnico Seventh",
                    Version = "v1",
                    Description = "Desafio Técnico Seventhr",
                    Contact = new OpenApiContact
                    {
                        Name = "seventh",
                        Url = new Uri("http://www.seventh.com.br")
                    }
                });

                c.IncludeXmlComments(System.String.Format(@"{0}API.xml",
                System.AppDomain.CurrentDomain.BaseDirectory));

            });
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //exemplo de V2
                //c.SwaggerEndpoint("/swagger/v2/swagger.json",
                // "Desafio Técnico Seventhr v2");
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                   "Desafio Técnico Seventhr v1");
            });
        }
    }
}
