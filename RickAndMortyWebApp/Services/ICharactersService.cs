using RickAndMorty.Domain.Models;
using RickAndMortyWebApp.Models.Dto;
using CharactersResult = RickAndMortyWebApp.Models.Dto.CharactersResult;

namespace RickAndMortyWebApp.Services;

public interface ICharactersService
{
    Task<CharactersResult> GetAllAsync();
}
