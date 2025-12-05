using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Infrastructure.HttpClients.RickAndMorty;
using RickAndMorty.Infrastructure.ServiceCollectionExtensions;

namespace RickAndMorty.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRickAndMortyApiClient, RickAndMortyApiClient>();
        services.AddExternalApiClients(configuration);
        services.AddAutoMapper(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.AddMaps(new[]
            {
        typeof(CharactersMappingProfile)
    });
        });


        return services;
    }
}

