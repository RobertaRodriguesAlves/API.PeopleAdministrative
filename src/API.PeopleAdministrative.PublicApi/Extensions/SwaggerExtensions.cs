using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace API.PeopleAdministrative.PublicApi.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API Administrative People",
                Contact = new OpenApiContact
                {
                    Name = "Roberta Suélen Rodrigues Alves"
                }
            });

            options.ResolveConflictingActions(apiDescription => apiDescription.FirstOrDefault());
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }

    public static void UseSwaggerAndUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.DisplayRequestDuration());
    }
}
