﻿using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;

public class RegisterTutorCommand : Command<RegisterTutorCommandResult>
{
    public RegisterTutorCommand(string email, string password, string firstName, string lastName)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; }

    public string Password { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
