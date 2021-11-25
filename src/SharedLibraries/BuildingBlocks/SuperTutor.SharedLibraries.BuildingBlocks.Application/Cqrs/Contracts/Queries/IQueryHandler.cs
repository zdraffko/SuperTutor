﻿using FluentResults;
using MediatR;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Queries;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Queries;

public interface IQueryHandler<in TQuery, TPayload> : IRequestHandler<TQuery, Result<TPayload>>
    where TQuery : Query<TPayload>
{
}
