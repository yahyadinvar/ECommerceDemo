using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerceDemo.API.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Endpoint üzerinde authorize var mı kontrol et
        bool hasAuthorize = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().Any();

        // Endpoint üzerinde allowAnonymous var mı kontrol et
        bool allowAnonymous = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any();

        // Eğer authorize ve allowAnonymous yoksa, Swagger’a security requirement ekleme, böylece kilit gözükmez
        if (!hasAuthorize || allowAnonymous)
            return;

        // Eğer authorize varsa, Swagger'da security requirement ekle
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            }] = new string[] { }
        });
    }
}
