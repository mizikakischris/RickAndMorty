using FluentResults;
using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Common.Utils;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Features.Queries;

public class CharactersQuery : IRequest<Result<CharactersDomainResponse>>
{
}

internal sealed class CharactersQueryHandler : IRequestHandler<CharactersQuery, Result<CharactersDomainResponse>>
{
    private readonly IRickAndMortyApiClient _client;

    public CharactersQueryHandler(IRickAndMortyApiClient client)
    {
        _client = client;
    }
    public async Task<Result<CharactersDomainResponse>> Handle(CharactersQuery request, CancellationToken cancellationToken)
    {
        return await ExceptionHandlingHelper.ExecuteWithApiExceptionHandling(async () =>
        {
            var charactersDomainResponse = await _client.GetCharactersAsync(cancellationToken);

            return Result.Ok(charactersDomainResponse);
        });
    }
}
