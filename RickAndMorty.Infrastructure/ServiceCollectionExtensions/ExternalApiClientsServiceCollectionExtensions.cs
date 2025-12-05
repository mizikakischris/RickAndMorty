using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RickAndMorty.Infrastructure.ServiceCollectionExtensions;

public static class ExternalApiClientsServiceCollectionExtensions
{
    public static IServiceCollection AddExternalApiClients(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddRickAndMortyClient(config);
        // other external api clients

        return services;
    }
}
