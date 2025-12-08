using AutoMapper;
using RickAndMorty.Domain.Models;
using RickAndMorty.Infrastructure.Models;

namespace RickAndMorty.Infrastructure.Profiles;

public class CharactersMappingProfile : Profile
{
    public CharactersMappingProfile()
    {
        CreateMap<CharactersResponseDto, CharactersDomainResponse>();
        CreateMap<CharacterDto, Character>();
        CreateMap<CharacterLocationDto, CharacterLocation>();
        CreateMap<PagingInfoDto, PagingInfo>();
        CreateMap<Infrastructure.Persistence.Entities.Character, Character>()
            .ForMember(dest => dest.Episode, opt => opt.MapFrom(src => src.Episodes.Select(e => e.EpisodeUrl)));

        CreateMap<Infrastructure.Persistence.Entities.CharacterLocation, CharacterLocation>();

        CreateMap<PagingInfo, PagingInfo>();
    }
}

