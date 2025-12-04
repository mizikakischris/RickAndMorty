using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Features.Queries;
using RickAndMorty.Common.Utils;
using RickAndMorty.WebApi.Responses;

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
    [ProducesResponseType(typeof(CharactersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CharactersListResponse>> GetCharacters(CancellationToken cancellationToken)
    {
        var charactersResult = await _mediator.Send(new CharactersQuery(), cancellationToken);

        var mappedResult = charactersResult.Map(characters =>
            new CharactersListResponse
            {
                Results = _mapper.Map<IEnumerable<CharactersResponse>>(characters)
            });

        return mappedResult.ToActionResult(HttpContext);
    }
}
