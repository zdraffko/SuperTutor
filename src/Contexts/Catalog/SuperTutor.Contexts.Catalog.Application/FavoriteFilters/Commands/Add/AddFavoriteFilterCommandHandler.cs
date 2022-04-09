using FluentResults;
using MediatR;

namespace SuperTutor.Contexts.Catalog.Application.FavoriteFilters.Commands.Add;

internal class AddFavoriteFilterCommandHandler : IRequestHandler<AddFavoriteFilterCommand, Result>
{
    public Task<Result> Handle(AddFavoriteFilterCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
