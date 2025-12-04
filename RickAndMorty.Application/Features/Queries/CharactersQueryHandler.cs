using FluentResults;
using MediatR;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application.Features.Queries;

public class CharactersQuery : IRequest<Result<IEnumerable<Character>>>
{
}

internal sealed class CharactersQueryHandler : IRequestHandler<CharactersQuery, Result<IEnumerable<Character>>>
{
    public Task<Result<IEnumerable<Character>>> Handle(CharactersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
