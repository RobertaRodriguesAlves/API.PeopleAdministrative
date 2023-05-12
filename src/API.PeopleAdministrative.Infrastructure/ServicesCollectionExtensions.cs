using API.PeopleAdministrative.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace API.PeopleAdministrative.Infrastructure;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Adiciona automaticamente todos os repositórios no ASP.NET Core DI que herdam a interface IRepository
        // REF: https://github.com/khellang/Scrutor
        services
            .Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IRepository>())
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
