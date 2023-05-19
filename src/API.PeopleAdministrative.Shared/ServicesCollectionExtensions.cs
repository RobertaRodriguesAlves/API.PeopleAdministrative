using API.PeopleAdministrative.Shared.AppSettings;
using API.PeopleAdministrative.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Shared;

[ExcludeFromCodeCoverage]
public static class ServicesCollectionExtensions
{
    public static void ConfigureAppSettings(this IServiceCollection services)
        => services
            .AddOptionsWithNonPublicProperties<ConnectionStrings>(nameof(ConnectionStrings));
}
