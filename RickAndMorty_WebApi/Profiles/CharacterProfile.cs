namespace RickAndMorty.WebApi.Profiles;

using AutoMapper;
using RickAndMorty.Domain.Models;
using RickAndMorty.WebApi.Responses;

public class CharacterMappingProfile : Profile
{
    public CharacterMappingProfile()
    {
        CreateMap<Character, CharactersResponse>().ReverseMap();
    }
}

