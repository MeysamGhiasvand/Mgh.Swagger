using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mgh.Swagger.Extensions;

public static class AddSwaggerExtensions
{
    public static void SwaggerDocOption(this SwaggerGenOptions swaggerGenOptions, OpenApiInfo openApiInfo)
    {
        Console.WriteLine("a");
        swaggerGenOptions.SwaggerDoc(openApiInfo.Title, openApiInfo);
        Console.WriteLine("b");
    }

    public static void XmlCommentOption(this SwaggerGenOptions swaggerGenOptions)
    {
        Console.WriteLine("c");
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly is null) return;
        var baseDirectory = AppContext.BaseDirectory;
        var commentsFileName = entryAssembly.GetName().Name + ".xml";
        var commentsFile = Path.Combine(baseDirectory, commentsFileName);
        if (File.Exists(commentsFile)) swaggerGenOptions.IncludeXmlComments(commentsFile);
        Console.WriteLine("d");
    }

    public static void AddSecurityDefinitionOption(this SwaggerGenOptions swaggerGenOptions,
        string addSecurityDefinitionTitle,
        OpenApiSecurityScheme openApiSecurityScheme)
    {
        swaggerGenOptions.AddSecurityDefinition(addSecurityDefinitionTitle, openApiSecurityScheme);
    }

    public static void AddSecurityRequirementOption(this SwaggerGenOptions swaggerGenOptions,
        OpenApiSecurityRequirement openApiSecurityRequirement)
    {
        swaggerGenOptions.AddSecurityRequirement(openApiSecurityRequirement);
    }
}