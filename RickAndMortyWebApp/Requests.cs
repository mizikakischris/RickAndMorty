namespace RickAndMortyWebApp;

public class APIRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
}

public enum ApiType
{
    GET,
    POST,
    PUT,
    DELETE
}