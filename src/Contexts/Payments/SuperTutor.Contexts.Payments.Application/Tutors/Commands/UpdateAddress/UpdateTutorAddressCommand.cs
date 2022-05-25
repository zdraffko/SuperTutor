using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateAddress;

public class UpdateTutorAddressCommand : Command
{
    public UpdateTutorAddressCommand(
        TutorId tutorId,
        string state,
        string city,
        string addressLineOne,
        string addressLineTwo,
        int postalCode)
    {
        TutorId = tutorId;
        State = state;
        City = city;
        AddressLineOne = addressLineOne;
        AddressLineTwo = addressLineTwo;
        PostalCode = postalCode;
    }

    public TutorId TutorId { get; }

    public string State { get; }

    public string City { get; }

    public string AddressLineOne { get; }

    public string AddressLineTwo { get; }

    public int PostalCode { get; }
}
