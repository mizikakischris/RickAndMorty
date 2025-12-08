using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Interfaces;

public interface ICharacterDataService
{
    Task StoreAliveCharactersAsync(CharactersDomainResponse characters, CancellationToken cancellationToken);
    Task<List<Domain.Models.Character>> GetAliveCharactersAsync(CancellationToken cancellationToken);
}
