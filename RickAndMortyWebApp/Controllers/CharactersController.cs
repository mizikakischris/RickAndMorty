using Microsoft.AspNetCore.Mvc;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Controllers;

public class CharactersController : Controller
{

    private readonly ICharactersService _charactersService;

    public CharactersController( ICharactersService charactersService)
    {

        _charactersService = charactersService;
    }

    // GET: /Characters
    public async Task<IActionResult> IndexCharacters()
    {
        var response = await _charactersService.GetAllAsync();

        return View("Index", response);
    }
}
