using API.PeopleAdministrative.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Diagnostics.CodeAnalysis;

namespace API.PeopleAdministrative.Application;

[ExcludeFromCodeCoverage]
public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // Adicionando automaticamente todos os serviços no ASP.NER Core DI que herdam a interface IAppService
        // REF: https://github.com/khellang/Scrutor
        services
            .Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IAppService>())
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
