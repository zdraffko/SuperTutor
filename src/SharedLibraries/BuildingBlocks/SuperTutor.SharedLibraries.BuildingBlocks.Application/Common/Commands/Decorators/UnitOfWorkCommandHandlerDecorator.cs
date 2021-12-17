﻿using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Contracts;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Application.Common.Commands.Decorators;

public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;

    public UnitOfWorkCommandHandlerDecorator(IUnitOfWork unitOfWork, ICommandHandler<TCommand> decoratedCommandHandler)
    {
        this.unitOfWork = unitOfWork;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);

        await unitOfWork.Commit(cancellationToken);

        return decoratedResult;
    }
}

public class UnitOfWorkCommandHandlerDecorator<TCommand, TPayload> : ICommandHandler<TCommand, TPayload>
    where TCommand : Command<TPayload>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICommandHandler<TCommand, TPayload> decoratedCommandHandler;

    public UnitOfWorkCommandHandlerDecorator(IUnitOfWork unitOfWork, ICommandHandler<TCommand, TPayload> decoratedCommandHandler)
    {
        this.unitOfWork = unitOfWork;
        this.decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<Result<TPayload>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var decoratedResult = await decoratedCommandHandler.Handle(command, cancellationToken);

        await unitOfWork.Commit(cancellationToken);

        return decoratedResult;
    }
}
