using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using System;
using System.Linq;
using System.Reflection;

namespace IrtWeb.Configuration
{
    internal static class SwaggerExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddSwaggerDocumentation()
            {
                return services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "IrtWeb",
                        Version = "v1",
                        Description = "IrtWeb API",
                    });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.CustomSchemaIds(type => type.FullName);

                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = System.IO.Path.Combine(baseDirectory, commentsFileName);
                    c.IncludeXmlComments(commentsFile);
                });
            }
        }

        extension(IApplicationBuilder app)
        {
            internal IApplicationBuilder UseSwaggerDocumentation()
            {
                return app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IrtWeb API V1");
                    });
            }
        }
    }
}
