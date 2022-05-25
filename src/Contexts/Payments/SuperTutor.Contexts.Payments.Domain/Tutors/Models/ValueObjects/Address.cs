using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class Address : ValueObject
{
    public Address(string state, string city, string lineOne, string lineTwo, int postalCode)
    {
        State = state;
        City = city;
        LineOne = lineOne;
        LineTwo = lineTwo;
        PostalCode = postalCode;
    }

    public string State { get; }

    public string City { get; }

    public string LineOne { get; }

    public string LineTwo { get; }

    public int PostalCode { get; }
}
