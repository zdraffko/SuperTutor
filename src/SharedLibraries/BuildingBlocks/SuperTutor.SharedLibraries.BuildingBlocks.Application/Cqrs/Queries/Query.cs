﻿using FluentResults;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Queries;

public abstract class Query<TPayload>
{
    protected Query() => Id = Guid.NewGuid();

    protected Query(Guid id) => Id = id;

    public Guid Id { get; }
}
