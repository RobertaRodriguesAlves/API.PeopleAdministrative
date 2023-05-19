using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
                    Name = "Roberta Suélen Rodrigues Alves",
                    Url = new Uri("rodriguesalves.roberta@gmail.com")
                }
            });

            options.ResolveConflictingActions(apiDescription => apiDescription.FirstOrDefault());

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, true);
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }

    public static void UseSwaggerAndUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.DisplayRequestDuration());
    }
}
