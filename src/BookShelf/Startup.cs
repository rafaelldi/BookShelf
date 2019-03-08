﻿using AutoMapper;
using BookShelf.Repository;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BookShelf
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
//            services.AddHealthChecks();
//            services.AddHealthChecksUI();
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
//            services.AddSwaggerGen(
//                options =>
//                {
//                    var provider = services.BuildServiceProvider()
//                        .GetRequiredService<IApiVersionDescriptionProvider>();
//
//                    foreach (var description in provider.ApiVersionDescriptions)
//                    {
//                        options.SwaggerDoc(description.GroupName, new Info
//                        {
//                            Title = $"BookShelf API {description.ApiVersion}",
//                            Version = description.ApiVersion.ToString()
//                        });
//                    }
//                });
            services.AddAutoMapper();
            services.Configure<MongoSettings>(Configuration.GetSection("MongoConnection"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
//            app.UseHealthChecks("/healthcheck", new HealthCheckOptions
//            {
//                Predicate = _ => true,
//                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//            });
//            app.UseHealthChecksUI();
//            app.UseSwagger();
//            app.UseSwaggerUI(
//                options =>
//                {
//                    foreach (var description in provider.ApiVersionDescriptions)
//                    {
//                        options.SwaggerEndpoint(
//                            $"/swagger/{description.GroupName}/swagger.json",
//                            description.GroupName.ToUpperInvariant());
//                    }
//                });
            app.UseMvc();
        }
    }
}