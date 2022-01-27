using FluentAssertions;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Tests.Domain.Extensions;

public static class ActionExtensions
{
    public static void ShouldBrakeInvariant<TBrokenInvariant>(this Action testAction)
        => testAction.Should().ThrowExactly<InvariantValidationException>()
            .And.BrokenInvariant.Should().BeOfType<TBrokenInvariant>();
}
