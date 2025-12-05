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
    }
}

