using FluentResults;
using MediatR;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Common.Utils;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Features.Queries;

public class CharactersQuery : IRequest<Result<CharactersDomainResponse>>
{
}

internal sealed class CharactersQueryHandler : IRequestHandler<CharactersQuery, Result<CharactersDomainResponse>>
{
    private readonly IRickAndMortyApiClient _client;
    private readonly ICharacterDataService _characterDataService;

    public CharactersQueryHandler(IRickAndMortyApiClient client, ICharacterDataService characterDataService)
    {
        _client = client;
        _characterDataService = characterDataService;
    }
    public async Task<Result<CharactersDomainResponse>> Handle(CharactersQuery request, CancellationToken cancellationToken)
    {
        return await ExceptionHandlingHelper.ExecuteWithApiExceptionHandling(async () =>
        {
            var charactersDomainResponse = await _client.GetCharactersAsync(cancellationToken);

            // Store only "Alive" characters
            await _characterDataService.StoreAliveCharactersAsync(charactersDomainResponse, cancellationToken);

            return Result.Ok(charactersDomainResponse);
        });
    }
}
