﻿using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;

public class UserId : GuidIdentifier
{
    public UserId(Guid value) : base(value)
    {
    }
}
