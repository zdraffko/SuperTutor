﻿namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountAddressInformation;

public class UpdateAccountAddressInformationRequest
{
    public UpdateAccountAddressInformationRequest(
        Guid tutorId,
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

    public Guid TutorId { get; }

    public string State { get; }

    public string City { get; }

    public string AddressLineOne { get; }

    public string AddressLineTwo { get; }

    public int PostalCode { get; }
}
