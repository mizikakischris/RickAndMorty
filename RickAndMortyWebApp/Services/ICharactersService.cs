using RickAndMortyWebApp.Models.Dto;

namespace RickAndMortyWebApp.Services;

public interface ICharactersService
{
    Task<CharactersDomainResponseDto> GetAllAsync();
}
