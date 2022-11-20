using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Mgh.Swagger;

public static class RegisterSwaggerServiceExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
        Action<ISwaggerConfig> options)
    {
        if (options == null)
        {
            throw new ArgumentException(nameof(options), "please provide config");
        }

        var config = new SwaggerConfig();
        options.Invoke(config);
        
        if (string.IsNullOrEmpty(config.OpenApiInfo.Description))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiInfo.Description), @"Description null");
        
        if (string.IsNullOrEmpty(config.OpenApiInfo.Version))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiInfo.Version), @"Version null");
        
        if (string.IsNullOrEmpty(config.OpenApiInfo.Title))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiInfo.Title), @"Title null");

        if (string.IsNullOrEmpty(config.XmlComments))
            throw new ArgumentNullException(nameof(SwaggerConfig.XmlComments), @"XmlComments null");
        
        if (string.IsNullOrEmpty(config.OpenApiSecurityScheme.Description))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiSecurityScheme.Description), @"SecurityDesc null");
        
        if (string.IsNullOrEmpty(config.OpenApiSecurityScheme.Name))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiSecurityScheme.Name), @"SecurityName is null");

        if (string.IsNullOrEmpty(config.OpenApiSecurityScheme.Scheme))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiSecurityScheme.Scheme), @"SecuritySchema is null");
        
        
        if (string.IsNullOrEmpty(config.OpenApiReference.Id))
            throw new ArgumentNullException(nameof(SwaggerConfig.OpenApiReference.Id), @"SecurityReferenceId is null");
        
        if (string.IsNullOrEmpty(config.AddSecurityDefinitionTitle))
            throw new ArgumentNullException(nameof(SwaggerConfig.AddSecurityDefinitionTitle), @"AddSecurityDefinitionTitle is null");
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = config.OpenApiInfo.Title,
                Version = config.OpenApiInfo.Version,
                Description = config.OpenApiInfo.Description
            });
            
            c.IncludeXmlComments(config.XmlComments);
            var securitySchema = new OpenApiSecurityScheme
            {
                Description =
                    config.OpenApiSecurityScheme.Description,
                Name = config.OpenApiSecurityScheme.Name,
                In = config.OpenApiSecurityScheme.In,
                Type = config.OpenApiSecurityScheme.Type,
                Scheme = config.OpenApiSecurityScheme.Scheme,
                Reference = new OpenApiReference
                {
                    Type = config.OpenApiReference.Type,
                    Id = config.OpenApiReference.Id
                }
            };

            c.AddSecurityDefinition(config.AddSecurityDefinitionTitle, securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {securitySchema, config.SecurityRequirementSchema}
            };

            c.AddSecurityRequirement(securityRequirement);
        });
        return services;
    }
}