﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountAddressInformation;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPayoutInformation;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class PaymentsController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private static readonly JsonSerializerOptions jsonSerializerOptions = new();

    private readonly string PaymentsApiUrl;

    static PaymentsController() => jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

    public PaymentsController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions) => PaymentsApiUrl = apiUrlsOptions.Value.Payments;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateAccountPersonalInformation(UpdateAccountPersonalInformationRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/funds/UpdateAccountPersonalInformation", request, cancellationToken: cancellationToken, options: jsonSerializerOptions);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateAccountAddressInformation(UpdateAccountAddressInformationRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/funds/UpdateAccountAddressInformation", request, cancellationToken: cancellationToken, options: jsonSerializerOptions);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateAccountPayoutInformation(UpdateAccountPayoutInformationRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/funds/UpdateAccountPayoutInformation", request, cancellationToken: cancellationToken, options: jsonSerializerOptions);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UploadVerificationDocuments(
        IFormFile identityDocumentFront,
        IFormFile identityDocumentBack,
        IFormFile addressDocument,
        CancellationToken cancellationToken)
    {
        var multipartFormDataContent = new MultipartFormDataContent
        {
            { new StringContent("acct_1L2FVk2YVsMu2Kbo"), "connectedAccountId" },
            { new StringContent("person_4L2FVk00Xp24cGdK"), "connectedPersonId" },
            { new StreamContent(identityDocumentFront.OpenReadStream()), nameof(identityDocumentFront), identityDocumentFront.FileName },
            { new StreamContent(identityDocumentBack.OpenReadStream()), nameof(identityDocumentBack), identityDocumentBack.FileName },
            { new StreamContent(addressDocument.OpenReadStream()), nameof(addressDocument), addressDocument.FileName }
        };

        var response = await httpClient.PostAsync($"{PaymentsApiUrl}/funds/UploadVerificationDocuments", multipartFormDataContent, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }
}