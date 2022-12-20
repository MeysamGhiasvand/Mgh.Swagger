using Microsoft.OpenApi.Models;

namespace Mgh.Swagger.Model;

public class SwaggerConfig 
{
    public OpenApiInfo OpenApiInfo { get; set; }
    public bool IsAuthorizationActive { get; set; }
    public OpenApiSecurityScheme OpenApiSecurityScheme { get; set; }
    public OpenApiReference OpenApiReference { get; set; }
    public OpenApiSecurityRequirement OpenApiSecurityRequirement { get; set; }
    public bool IsXmlCommentsActive { get; set; }
    public string AddSecurityDefinitionTitle { get; set; }
    public string[] SecurityRequirementSchema { get; set; }
}