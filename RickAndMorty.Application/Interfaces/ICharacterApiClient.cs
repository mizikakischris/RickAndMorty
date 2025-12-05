using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Interfaces;

public interface IRickAndMortyApiClient
{
    public Task<CharactersDomainResponse> GetCharactersAsync(CancellationToken cancellationToken);
}
