using Mgh.Swagger.Model;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mgh.Swagger.Extensions;

public static class RegisterSwaggerServiceExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
        Action<SwaggerGenOptions> swaggerGenOptions, SwaggerConfig swaggerConfig)
    {
        if (swaggerGenOptions == null)
            throw new ArgumentException(nameof(swaggerConfig));

        var config = new SwaggerGenOptions();
        swaggerGenOptions.Invoke(config);


        try
        {
            services.AddSwaggerGen(c =>
            {
                if (swaggerConfig.OpenApiInfo is not null)
                    c.SwaggerDocOption(swaggerConfig.OpenApiInfo);

                if (swaggerConfig.IsXmlCommentsActive)
                    c.XmlCommentOption();
            
                // if (swaggerConfig.IsAuthorizationActive)
                // {
                //     c.AddSecurityDefinitionOption(swaggerConfig.AddSecurityDefinitionTitle,
                //         swaggerConfig.OpenApiSecurityScheme);
                //
                //     c.AddSecurityRequirementOption(swaggerConfig.OpenApiSecurityRequirement);
                // }
            });
            return services;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
      
    }
}