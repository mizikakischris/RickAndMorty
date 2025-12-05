namespace RickAndMortyWebApp.Services;

public interface IBaseService
{

    Task<T> SendAsync<T>(APIRequest apiRequest);
}
