using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Features.Queries;
using MediatR;

namespace RickAndMorty.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CharactersQueryHandler).Assembly);
        });

        return services;
    }
}