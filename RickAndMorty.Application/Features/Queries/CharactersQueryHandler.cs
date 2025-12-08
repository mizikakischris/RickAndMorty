using FluentResults;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Common.Utils;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Features.Queries;

public class CharactersQuery : IRequest<Result<CharactersResult>>
{
}

public class CharactersQueryHandler
    : IRequestHandler<CharactersQuery, Result<CharactersResult>>
{
    private readonly IRickAndMortyApiClient _client;
    private readonly ICharacterDataService _characterDataService;
    private readonly IMemoryCache _memoryCache;

    private const string CacheKey = "CharactersCache";
    private const string CacheInvalidatedKey = "CharactersCacheInvalidated";

    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public CharactersQueryHandler(
        IRickAndMortyApiClient client,
        ICharacterDataService dataService,
        IMemoryCache memoryCache)
    {
        _client = client;
        _characterDataService = dataService;
        _memoryCache = memoryCache;
    }

    public async Task<Result<CharactersResult>> Handle(
    CharactersQuery request,
    CancellationToken cancellationToken)
    {
        return await ExceptionHandlingHelper.ExecuteWithApiExceptionHandling(async () =>
        {
            bool fromDatabase = false;
            _memoryCache.TryGetValue(CacheKey, out CharactersCacheInfo? cacheInfo);
            bool expired = cacheInfo is null ||
                           (DateTime.UtcNow - cacheInfo.LastFetchUtc) > CacheDuration;

            bool invalidated = _memoryCache.Get<bool>(CacheInvalidatedKey);

            List<Character> charactersList;

            if (!expired && !invalidated && cacheInfo != null)
            {
                charactersList = cacheInfo.Characters;
            }
            else
            {
                // Cache expired  fetch from API
                var externalCharacters = await _client.GetCharactersAsync(cancellationToken);

                // Clear db and save alive characters
                await _characterDataService.StoreAliveCharactersAsync(externalCharacters, cancellationToken);

                // Read fresh from DB
                charactersList = await _characterDataService.GetAliveCharactersAsync(cancellationToken);
                fromDatabase = true;
                // Update cache
                _memoryCache.Set(CacheKey, new CharactersCacheInfo
                {
                    LastFetchUtc = DateTime.UtcNow,
                    Characters = charactersList
                });
                _memoryCache.Set(CacheInvalidatedKey, false);
            }

            return Result.Ok(new CharactersResult
            {
                Characters = charactersList,
                FromDatabase = fromDatabase
            });
        });
    }
}
