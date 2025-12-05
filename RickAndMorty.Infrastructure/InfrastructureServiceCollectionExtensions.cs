using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Infrastructure.HttpClients.RickAndMorty;
using RickAndMorty.Infrastructure.Persistence.EFRepository;
using RickAndMorty.Infrastructure.ServiceCollectionExtensions;
using RickAndMorty.Infrastructure.Services;

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
        }, typeof(CharactersMappingProfile).Assembly);
        services.AddScoped<ICharacterDataService, CharacterDataService>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        return services;
    }
}

