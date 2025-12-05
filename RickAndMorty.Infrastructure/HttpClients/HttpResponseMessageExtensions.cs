namespace RickAndMorty.Infrastructure.HttpClients;

public static class HttpResponseMessageExtensions
{
    public static async Task ThrowApiException(this HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        throw new HttpRequestException(
            $"Request to {response.RequestMessage?.RequestUri} failed with status code {response.StatusCode}. Response: {content}"
        );
    }
}
