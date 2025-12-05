using System.Text;
using Newtonsoft.Json;

namespace RickAndMortyWebApp.Services;

public class BaseService : IBaseService
{
    public IHttpClientFactory httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            var client = httpClient.CreateClient("RickAndMortyAPI");
            var message = new HttpRequestMessage
            {
                RequestUri = new Uri(apiRequest.Url),
                Method = apiRequest.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                }
            };
            message.Headers.Add("Accept", "application/json");

            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(
                    JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            var apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();


            var result = JsonConvert.DeserializeObject<T>(apiContent);

            return result!;
        }
        catch (Exception e)
        {

            throw new ApplicationException($"API call failed: {e.Message}");
        }
    }
}

