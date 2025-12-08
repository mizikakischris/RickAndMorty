using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using RickAndMorty.Application;
using RickAndMorty.Application.Features.Queries;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Tests;

[TestClass]
public class CharactersQueryHandlerTests
{
    private IRickAndMortyApiClient _fakeClient = null!;
    private ICharacterDataService _fakeDataService = null!;
    private IMemoryCache _fakeMemoryCache = null!;
    private CharactersQueryHandler _handler = null!;

    private readonly CharactersDomainResponse _sampleApiResponse = new CharactersDomainResponse
    {
        CharactersList = new List<Character>
        {
            new() { Id = 1, Name = "Rick" },
            new () { Id = 2, Name = "Morty" }
        }
    };

    [TestInitialize]
    public void Setup()
    {
        _fakeClient = A.Fake<IRickAndMortyApiClient>();
        _fakeDataService = A.Fake<ICharacterDataService>();
        _fakeMemoryCache = A.Fake<IMemoryCache>();

        _handler = new CharactersQueryHandler(_fakeClient, _fakeDataService, _fakeMemoryCache);
    }

    [TestMethod]
    public async Task Handle_CacheExpired_CallsApiAndStoresCharacters()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        _handler = new CharactersQueryHandler(_fakeClient, _fakeDataService, memoryCache);

        A.CallTo(() => _fakeClient.GetCharactersAsync(A<CancellationToken>._))
            .Returns(Task.FromResult(_sampleApiResponse));

        A.CallTo(() => _fakeDataService.StoreAliveCharactersAsync(_sampleApiResponse, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        A.CallTo(() => _fakeDataService.GetAliveCharactersAsync(A<CancellationToken>._))
            .Returns(Task.FromResult(_sampleApiResponse.CharactersList.ToList()));

        // Act
        var result = await _handler.Handle(new CharactersQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Characters.Should().BeEquivalentTo(_sampleApiResponse.CharactersList);
        result.Value.FromDatabase.Should().BeTrue();

        A.CallTo(() => _fakeClient.GetCharactersAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeDataService.StoreAliveCharactersAsync(_sampleApiResponse, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeDataService.GetAliveCharactersAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task Handle_CacheInvalidated_CallsApiAndUpdatesCache()
    {
        // Arrange: use real MemoryCache
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        _handler = new CharactersQueryHandler(_fakeClient, _fakeDataService, memoryCache);

        var cachedCharacters = _sampleApiResponse.CharactersList.ToList();
        var cacheInfo = new CharactersCacheInfo
        {
            LastFetchUtc = DateTime.UtcNow,
            Characters = cachedCharacters
        };

        memoryCache.Set("CharactersCache", cacheInfo);
        memoryCache.Set("CharactersCacheInvalidated", true);

        A.CallTo(() => _fakeClient.GetCharactersAsync(A<CancellationToken>._))
            .Returns(Task.FromResult(_sampleApiResponse));
        A.CallTo(() => _fakeDataService.StoreAliveCharactersAsync(_sampleApiResponse, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeDataService.GetAliveCharactersAsync(A<CancellationToken>._))
            .Returns(Task.FromResult(_sampleApiResponse.CharactersList.ToList()));

        // Act
        var result = await _handler.Handle(new CharactersQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Characters.Should().BeEquivalentTo(_sampleApiResponse.CharactersList);
        result.Value.FromDatabase.Should().BeTrue();

        A.CallTo(() => _fakeClient.GetCharactersAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeDataService.StoreAliveCharactersAsync(_sampleApiResponse, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeDataService.GetAliveCharactersAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }
}