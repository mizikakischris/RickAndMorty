using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RickAndMorty.Infrastructure.Configuratoin;
using RickAndMorty.Infrastructure.Services;

namespace RickAndMorty.Infrastructure.ServiceCollectionExtensions;

public static class RickAndMortyServiceCollectionExtensions
{
    public static IServiceCollection AddRickAndMortyClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RickAndMortyOptions>(
            configuration.GetSection("RickAndMortyOptions"));

        services.AddHttpClient<HttpRequestService>((sp, client) =>
        {
            var options = sp.GetRequiredService<
                IOptions<RickAndMortyOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler()
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
                };
            })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }
}