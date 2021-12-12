using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Swagger
{
    public class Settings
    {
        private const string NAME_SYSTEM = "Desafio Técnico Seventh";
        private static readonly string[] VERSIONS = { "v1" };
        public static void ConfigureServicesSwagger(IServiceCollection services)
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
                //c.SchemaFilter<SwaggerSchemaFilter>();
                c.IncludeXmlComments(String.Format(@"{0}API.xml",
                System.AppDomain.CurrentDomain.BaseDirectory));
            });
        }
        public static void ConfigureSwagger(IApplicationBuilder app)
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
