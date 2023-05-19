using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.PeopleAdministrative.Shared.Extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddOptionsWithNonPublicProperties<TOptions>(this IServiceCollection services, string configSectionPath) where TOptions : class
    {
        OptionsBuilder<TOptions> optionsBuilder = services.AddOptions<TOptions>().BindConfiguration(configSectionPath, delegate (BinderOptions options)
        {
            options.BindNonPublicProperties = true;
        }).ValidateDataAnnotations()
          .ValidateOnStart();
        return optionsBuilder.Services;
    }
}
