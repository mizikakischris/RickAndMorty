using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Features.Queries;

namespace RickAndMorty.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CharactersQueryHandler).Assembly);
       
        return services;
    }
}