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
            bool fromDatabase = true;

            bool invalidated = _memoryCache.Get<bool>(CacheInvalidatedKey);

            CharactersCacheEntry? entry = null;
            _memoryCache.TryGetValue(CacheKey, out entry);

            bool expired = entry is null ||
                           (DateTime.UtcNow - entry.CachedAt) > CacheDuration;

            CharactersDomainResponse characters;

            if (!invalidated && !expired)
            {
                characters = entry!.Data;
            }
            else
            {
                // Fetch new data from external API
                characters = await _client.GetCharactersAsync(cancellationToken);

                // Store alive characters in DB
                await _characterDataService.StoreAliveCharactersAsync(characters, cancellationToken);

                // Update cache with timestamp
                var newEntry = new CharactersCacheEntry
                {
                    Data = characters,
                    CachedAt = DateTime.UtcNow
                };

                _memoryCache.Set(CacheKey, newEntry);
                _memoryCache.Set(CacheInvalidatedKey, false);

                fromDatabase = false;
            }

            return Result.Ok(new CharactersResult
            {
                Characters = characters,
                FromDatabase = fromDatabase
            });
        });
    }
}
