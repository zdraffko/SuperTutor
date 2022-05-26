using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.AcceptTermsOfService;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateAddress;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateBankAccount;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UploadVerificationDocuments;
using SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreTermsOfServiceAccepted;
using SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreVerificationDocumentsCollected;
using SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsAddressInformationCollected;
using SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsBankAccountInformationCollected;
using SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsPersonalInformationCollected;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Api.Tutors.Controllers;

public class TutorsController : ApiController
{
    private readonly ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler;
    private readonly ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler;
    private readonly ICommandHandler<UpdateTutorBankAccountCommand> updateTutorBankAccountCommandHandler;
    private readonly ICommandHandler<UploadTutorVerificationDocumentsCommand> uploadTutorVerificationDocumentsCommandHandler;
    private readonly ICommandHandler<AcceptTutorTermsOfServiceCommand> acceptTutorTermsOfServiceCommandHandler;
    private readonly IQueryHandler<GetTutorAreTermsOfServiceAcceptedQuery, GetTutorAreTermsOfServiceAcceptedQueryPayload> getTutorAreTermsOfServiceAcceptedQueryHandler;
    private readonly IQueryHandler<GetTutorAreVerificationDocumentsCollectedQuery, GetTutorAreVerificationDocumentsCollectedQueryPayload> getTutorAreVerificationDocumentsCollectedQueryHandler;
    private readonly IQueryHandler<GetTutorIsAddressInformationCollectedQuery, GetTutorIsAddressInformationCollectedQueryPayload> getTutorIsAddressInformationCollectedQueryHandler;
    private readonly IQueryHandler<GetTutorIsBankAccountInformationCollectedQuery, GetTutorIsBankAccountInformationCollectedQueryPayload> getTutorIsBankAccountInformationCollectedQueryHandler;
    private readonly IQueryHandler<GetTutorIsPersonalInformationCollectedQuery, GetTutorIsPersonalInformationCollectedQueryPayload> getTutorIsPersonalInformationCollectedQueryHandler;

    public TutorsController(
        ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler,
        ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler,
        ICommandHandler<UpdateTutorBankAccountCommand> updateTutorBankAccountCommandHandler,
        ICommandHandler<UploadTutorVerificationDocumentsCommand> uploadTutorVerificationDocumentsCommandHandler,
        ICommandHandler<AcceptTutorTermsOfServiceCommand> acceptTutorTermsOfServiceCommandHandler,
        IQueryHandler<GetTutorAreTermsOfServiceAcceptedQuery, GetTutorAreTermsOfServiceAcceptedQueryPayload> getTutorAreTermsOfServiceAcceptedQueryHandler,
        IQueryHandler<GetTutorAreVerificationDocumentsCollectedQuery, GetTutorAreVerificationDocumentsCollectedQueryPayload> getTutorAreVerificationDocumentsCollectedQueryHandler,
        IQueryHandler<GetTutorIsAddressInformationCollectedQuery, GetTutorIsAddressInformationCollectedQueryPayload> getTutorIsAddressInformationCollectedQueryHandler,
        IQueryHandler<GetTutorIsBankAccountInformationCollectedQuery, GetTutorIsBankAccountInformationCollectedQueryPayload> getTutorIsBankAccountInformationCollectedQueryHandler,
        IQueryHandler<GetTutorIsPersonalInformationCollectedQuery, GetTutorIsPersonalInformationCollectedQueryPayload> getTutorIsPersonalInformationCollectedQueryHandler)
    {
        this.updateTutorPersonalInformationCommandHandler = updateTutorPersonalInformationCommandHandler;
        this.updateTutorAddressCommandHandler = updateTutorAddressCommandHandler;
        this.updateTutorBankAccountCommandHandler = updateTutorBankAccountCommandHandler;
        this.uploadTutorVerificationDocumentsCommandHandler = uploadTutorVerificationDocumentsCommandHandler;
        this.acceptTutorTermsOfServiceCommandHandler = acceptTutorTermsOfServiceCommandHandler;
        this.getTutorAreTermsOfServiceAcceptedQueryHandler = getTutorAreTermsOfServiceAcceptedQueryHandler;
        this.getTutorAreVerificationDocumentsCollectedQueryHandler = getTutorAreVerificationDocumentsCollectedQueryHandler;
        this.getTutorIsAddressInformationCollectedQueryHandler = getTutorIsAddressInformationCollectedQueryHandler;
        this.getTutorIsBankAccountInformationCollectedQueryHandler = getTutorIsBankAccountInformationCollectedQueryHandler;
        this.getTutorIsPersonalInformationCollectedQueryHandler = getTutorIsPersonalInformationCollectedQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult> UpdatePersonalInformation(UpdateTutorPersonalInformationCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorPersonalInformationCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAddress(UpdateTutorAddressCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorAddressCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateBankAccount(UpdateTutorBankAccountCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorBankAccountCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UploadVerificationDocuments(
        [FromForm] TutorId tutorId,
        IFormFile identityDocumentFront,
        IFormFile identityDocumentBack,
        IFormFile addressDocument,
        CancellationToken cancellationToken)
    {
        var uploadTutorVerificationDocumentsCommand = new UploadTutorVerificationDocumentsCommand(
            tutorId,
            identityDocumentFront.OpenReadStream(),
            identityDocumentBack.OpenReadStream(),
            addressDocument.OpenReadStream());

        var uploadVerificationDocumentsCommandResult = await uploadTutorVerificationDocumentsCommandHandler.Handle(uploadTutorVerificationDocumentsCommand, cancellationToken);

        return uploadVerificationDocumentsCommandResult.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> AcceptTermsOfService(AcceptTutorTermsOfServiceCommand command, CancellationToken cancellationToken)
        => await Handle(acceptTutorTermsOfServiceCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorAreVerificationDocumentsCollectedQueryPayload>> GetAreVerificationDocumentsCollected([FromJsonQuery] GetTutorAreVerificationDocumentsCollectedQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorAreVerificationDocumentsCollectedQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorIsAddressInformationCollectedQueryPayload>> GetIsAddressInformationCollected([FromJsonQuery] GetTutorIsAddressInformationCollectedQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorIsAddressInformationCollectedQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorAreTermsOfServiceAcceptedQueryPayload>> GetAreTermsOfServiceAccepted([FromJsonQuery] GetTutorAreTermsOfServiceAcceptedQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorAreTermsOfServiceAcceptedQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorIsBankAccountInformationCollectedQueryPayload>> GetIsBankAccountInformationCollected([FromJsonQuery] GetTutorIsBankAccountInformationCollectedQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorIsBankAccountInformationCollectedQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorIsPersonalInformationCollectedQueryPayload>> GetIsPersonalInformationCollected([FromJsonQuery] GetTutorIsPersonalInformationCollectedQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorIsPersonalInformationCollectedQueryHandler, query, cancellationToken);
}
