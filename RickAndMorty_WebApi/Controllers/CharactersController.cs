using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Features.Queries;
using RickAndMorty.Domain.Models;
using RickAndMorty.WebApi.Utils;

namespace RickAndMorty.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CharactersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CharactersDomainResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<CharactersResult>> GetCharacters(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CharactersQuery(), cancellationToken);

        if (result.IsSuccess && result.Value != null)
        {
            Response.Headers["from-database"] = result.Value.FromDatabase ? "true" : "false";
        }
        return result.ToActionResult(HttpContext);
    }
}
