using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Constants;
using SuperTutor.ApiGateways.Web.Models.Payments.CreateCharge;
using SuperTutor.ApiGateways.Web.Models.Payments.GetAreTutorTermsOfServiceAccepted;
using SuperTutor.ApiGateways.Web.Models.Payments.GetAreTutorVerificationDocumentsCollected;
using SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorAddressInformationCollected;
using SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorBankAccountInformationCollected;
using SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorPersonalInformationCollected;
using SuperTutor.ApiGateways.Web.Models.Payments.GetTransfersForTutor;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountAddressInformation;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPayoutInformation;
using SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class PaymentsController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private static readonly JsonSerializerOptions jsonSerializerOptions = new();

    private readonly string PaymentsApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    static PaymentsController() => jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

    public PaymentsController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        PaymentsApiUrl = apiUrlsOptions.Value.Payments;
        this.httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpPost]
    public async Task<ActionResult> UpdateTutorPersonalInformation(UpdateAccountPersonalInformationRequest request, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId,
            request.DateOfBirth
        };

        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/tutors/UpdatePersonalInformation", paymentsRequest, cancellationToken: cancellationToken, options: jsonSerializerOptions);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpPost]
    public async Task<ActionResult> UpdateTutorAddressInformation(UpdateAccountAddressInformationRequest request, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId,
            request.State,
            request.City,
            request.AddressLineOne,
            request.AddressLineTwo,
            request.PostalCode
        };

        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/tutors/UpdateAddress", paymentsRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpPost]
    public async Task<ActionResult> UpdateTutorPayoutInformation(UpdateAccountPayoutInformationRequest request, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId,
            request.BankAccountHolderFullName,
            request.BankAccountHolderType,
            request.BankAccountIban
        };

        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/tutors/UpdateBankAccount", paymentsRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpPost]
    public async Task<ActionResult> UploadTutorVerificationDocuments(
        IFormFile identityDocumentFront,
        IFormFile identityDocumentBack,
        IFormFile addressDocument,
        CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var multipartFormDataContent = new MultipartFormDataContent
        {
            { new StringContent(tutorId), "tutorId" },
            { new StreamContent(identityDocumentFront.OpenReadStream()), nameof(identityDocumentFront), identityDocumentFront.FileName },
            { new StreamContent(identityDocumentBack.OpenReadStream()), nameof(identityDocumentBack), identityDocumentBack.FileName },
            { new StreamContent(addressDocument.OpenReadStream()), nameof(addressDocument), addressDocument.FileName }
        };

        var response = await httpClient.PostAsync($"{PaymentsApiUrl}/tutors/UploadVerificationDocuments", multipartFormDataContent, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpPost]
    public async Task<ActionResult> AcceptTutorTermsOfService(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId,
            IpOfAcceptance = Request.HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/tutors/AcceptTermsOfService", paymentsRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<bool>> GetAreTutorVerificationDocumentsCollected(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/tutors/GetAreVerificationDocumentsCollected?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetAreTutorVerificationDocumentsCollectedResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<bool>> GetAreTutorTermsOfServiceAccepted(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/tutors/GetAreTermsOfServiceAccepted?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetAreTutorTermsOfServiceAcceptedResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<bool>> GetIsTutorAddressInformationCollected(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/tutors/GetIsAddressInformationCollected?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetIsTutorAddressInformationCollectedResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<bool>> GetIsTutorPersonalInformationCollected(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/tutors/GetIsPersonalInformationCollected?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetIsTutorPersonalInformationCollectedResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<bool>> GetIsTutorBankAccountInformationCollected(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/tutors/GetIsBankAccountInformationCollected?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetIsTutorBankAccountInformationCollectedResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpPost]
    public async Task<ActionResult<CreateChargeResponse>> CreateCharge(CreateChargeRequest request, CancellationToken cancellationToken)
    {
        var studentId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (studentId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            request.ChargeAmount,
            request.LessonId,
            request.TutorId,
            StudentId = studentId
        };

        var response = await httpClient.PostAsJsonAsync($"{PaymentsApiUrl}/charges/Create", paymentsRequest, cancellationToken: cancellationToken, options: jsonSerializerOptions);

        if (response.IsSuccessStatusCode)
        {
            var responsePayload = await response.Content.ReadFromJsonAsync<CreateChargeResponse>(cancellationToken: cancellationToken);

            return Ok(responsePayload);
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize(Roles = Roles.Tutor)]
    [HttpGet]
    public async Task<ActionResult<GetTransfersForTutorResponse>> GetTransfersForTutor(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var paymentsRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{PaymentsApiUrl}/transfers/GetForTutor?query={JsonSerializer.Serialize(paymentsRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetTransfersForTutorResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }
}
