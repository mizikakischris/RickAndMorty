using AutoMapper;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Domain.Models;
using RickAndMorty.Infrastructure.Persistence.Entities;
using Character = RickAndMorty.Infrastructure.Persistence.Entities.Character;
using CharacterLocation = RickAndMorty.Infrastructure.Persistence.Entities.CharacterLocation;

namespace RickAndMorty.Infrastructure.Services;

public class CharacterDataService : ICharacterDataService
{
    private readonly IRepository<Character> _characterRepository;
    private readonly IRepository<CharacterEpisode> _characterEpisodeRepository;
    private readonly IRepository<CharacterLocation> _characterLocationRepository;
    private readonly IMapper _mapper;

    public CharacterDataService(
        IRepository<Character> characterRepository,
        IRepository<CharacterEpisode> characterEpisodeRepository,
        IRepository<CharacterLocation> characterLocationRepository,
        IMapper mapper)
    {
        _characterRepository = characterRepository;
        _characterEpisodeRepository = characterEpisodeRepository;
        _characterLocationRepository = characterLocationRepository;
        _mapper = mapper;
    }

    public async Task<List<Domain.Models.Character>> GetAliveCharactersAsync(CancellationToken cancellationToken)
    {
        var dbCharacters = await _characterRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<Domain.Models.Character>>(dbCharacters);
    }

    public async Task StoreAliveCharactersAsync(CharactersDomainResponse characters, CancellationToken cancellationToken)
    {

        var aliveCharactersApi = characters.CharactersList
            .Where(c => string.Equals(c.Status, "Alive", StringComparison.OrdinalIgnoreCase))
            .ToList();


        var oldEpisodes = await _characterEpisodeRepository.GetAllAsync(cancellationToken);
        var oldCharacters = await _characterRepository.GetAllAsync(cancellationToken);
        var oldLocations = await _characterLocationRepository.GetAllAsync(cancellationToken);

        _characterEpisodeRepository.RemoveRange(oldEpisodes);
        _characterRepository.RemoveRange(oldCharacters);
        _characterLocationRepository.RemoveRange(oldLocations);

        await _characterRepository.SaveChangesAsync(cancellationToken);

 
        var locations = aliveCharactersApi
            .Select(c => c.Origin)
            .Concat(aliveCharactersApi.Select(c => c.Location))
            .GroupBy(l => l.Name)
            .Select(g => new CharacterLocation
            {
                Name = g.First().Name,
                Url = g.First().Url
            })
            .ToList();

        await _characterLocationRepository.AddRangeAsync(locations, cancellationToken);
        await _characterLocationRepository.SaveChangesAsync(cancellationToken);


        var charactersEntities = aliveCharactersApi.Select(c =>
        {
            var origin = locations.First(l => l.Name == c.Origin.Name);
            var location = locations.First(l => l.Name == c.Location.Name);

            return new Character
            {
                Name = c.Name,
                Status = c.Status,
                Species = c.Species,
                Type = c.Type,
                Gender = c.Gender,
                Image = c.Image,
                Url = c.Url,
                Created = c.Created,
                OriginId = origin.Id,
                LocationId = location.Id
            };
        }).ToList();

        await _characterRepository.AddRangeAsync(charactersEntities, cancellationToken);
        await _characterRepository.SaveChangesAsync(cancellationToken); 


        var characterEpisodes = aliveCharactersApi
            .SelectMany((c, index) => c.Episode.Select(ep => new CharacterEpisode
            {
                CharacterId = charactersEntities[index].Id, 
                EpisodeUrl = ep
            }))
            .ToList();

        await _characterEpisodeRepository.AddRangeAsync(characterEpisodes, cancellationToken);
        await _characterRepository.SaveChangesAsync(cancellationToken);
    }
}
