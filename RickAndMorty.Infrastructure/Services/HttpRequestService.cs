using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RickAndMorty.Infrastructure.HttpClients;

namespace RickAndMorty.Infrastructure.Services;

public class HttpRequestService
{
    private readonly HttpClient _client;
    private readonly ILogger<HttpRequestService> _logger;

    public HttpRequestService(HttpClient client, ILogger<HttpRequestService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken) where T : class
    {
        HttpResponseMessage response;

        try
        {
            response = await _client.GetAsync(endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HTTP request failed to {Endpoint}", endpoint);
            throw;
        }

        if (!response.IsSuccessStatusCode)
        {
            var content = await ReadResponseContentAsync(response, cancellationToken);
            _logger.LogWarning(
                "Failed to fetch data from {Endpoint}. Status code: {StatusCode}. Response: {Response}",
                endpoint, response.StatusCode, content
            );

            await response.ThrowApiException(cancellationToken);
        }

        var responseContent = await ReadResponseContentAsync(response, cancellationToken);

        var data = JsonConvert.DeserializeObject<T>(
            responseContent,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            })
            ?? throw new JsonException($"Failed to deserialize response from {endpoint}: {responseContent}");

        return data;
    }

    protected static async Task<string> ReadResponseContentAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        return string.IsNullOrEmpty(responseContent)
            ? throw new InvalidOperationException($"The response from {response.RequestMessage?.RequestUri} is null or empty.")
            : responseContent;
    }
}
