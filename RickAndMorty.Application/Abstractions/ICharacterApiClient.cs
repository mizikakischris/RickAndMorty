using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Abstractions;

public interface IRickAndMortyApiClient
{
   public Task<CharactersDomainResponse> GetCharactersAsync(CancellationToken cancellationToken);
}
