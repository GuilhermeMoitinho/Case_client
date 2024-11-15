using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CaseClient.WebAPI.Configuration;

internal static class SwaggerConfig
{
    internal static WebApplicationBuilder AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Case Client",
                    Version = "v1",
                    Description =
                        """
                        OpenAPI specification for testing and consuming Case Client

                        ## Resources

                        * https://github.com/OAI/OpenAPI-Specification
                        \
                        _© Guilherme Moitinho (https://github.com/GuilhermeMoitinho)
                        """
                });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
               $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return builder;
    }
}