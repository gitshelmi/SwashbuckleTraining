using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SwaggerTraining.DataAccess;
using SwaggerTraining.Filters;

namespace SwaggerTraining
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                // return 406 if the return type is not supported
                options.ReturnHttpNotAcceptable = true;
                // support XML (JSON is supported by default)
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=SwaggerDB;Trusted_Connection=True;");
            });

            // set the group name format
            services.AddVersionedApiExplorer(configure =>
            {
                configure.GroupNameFormat = "'v'VV"; // e.g., v1 or v1.2
            });

            services.AddApiVersioning(config =>
            {
                // include the version in the response headers
                config.ReportApiVersions = true;
            });

            // for accessing the versioning information
            var apiVersionDescriptionProvider =
                services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(config =>
            {
                // register one generator per version
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    config.SwaggerDoc($"SwaggerTrainingDocument{description.GroupName}",
                        new OpenApiInfo
                        {
                            Version = description.ApiVersion.ToString(),
                            Title = "Swagger Training API",
                            Description = "A demo application for Swagger and Swashbuckle.",
                            TermsOfService = new Uri("https://example.com/terms"),
                            Contact = new OpenApiContact
                            {
                                Name = "Jon Snow",
                                Email = "jon.snow@got.example",
                                Url = new Uri("https://en.wikipedia.org/wiki/Jon_Snow_(character)"),
                            },
                            License = new OpenApiLicense
                            {
                                Name = "Winterfell License",
                                Url = new Uri("https://en.wikipedia.org/wiki/Winterfell_(Game_of_Thrones_episode)")
                            }
                        });

                    config.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                }

                // use the version for grouping the actions
                config.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                        .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(version =>
                            $"SwaggerTrainingDocumentv{version}" == documentName);
                    }

                    return actionApiVersionModel.ImplementedApiVersions.Any(version =>
                        $"SwaggerTrainingDocumentv{version}" == documentName);
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error happened. Please try again later.");
                    });
                });
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(config =>
            {
                // add one endpoint per version
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint($"/swagger/SwaggerTrainingDocument{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                config.RoutePrefix = "";
            });

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
