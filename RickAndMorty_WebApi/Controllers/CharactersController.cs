using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Features.Queries;
using RickAndMorty.Common.Utils;
using RickAndMorty.Domain.Models;
using RickAndMorty.WebApi.Utils;

namespace RickAndMorty.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CharactersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CharactersDomainResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CharactersDomainResponse>> GetCharacters(CancellationToken cancellationToken)
    {
        var charactersResult = await _mediator.Send(new CharactersQuery(), cancellationToken);

        return charactersResult.ToActionResult(HttpContext);
    }
}
