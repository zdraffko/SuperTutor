using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms.Invariants;

internal class ClassroomCanOnlyBeJoinedWhenItIsActiveInvariant : Invariant
{
    private readonly bool IsClassroomActive;

    public ClassroomCanOnlyBeJoinedWhenItIsActiveInvariant(bool isClassroomActive) : base("Класната стая не е активна") => IsClassroomActive = isClassroomActive;

    public override bool IsValid() => IsClassroomActive;
}
