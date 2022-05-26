using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateAddress;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateBankAccount;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UploadVerificationDocuments;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Api.Tutors.Controllers;

public class TutorsController : ApiController
{
    private readonly ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler;
    private readonly ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler;
    private readonly ICommandHandler<UpdateTutorBankAccountCommand> updateTutorBankAccountCommandHandler;
    private readonly ICommandHandler<UploadVerificationDocumentsCommand> uploadVerificationDocumentsCommandHandler;

    public TutorsController(ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler, ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler, ICommandHandler<UpdateTutorBankAccountCommand> updateTutorBankAccountCommandHandler, ICommandHandler<UploadVerificationDocumentsCommand> uploadVerificationDocumentsCommandHandler)
    {
        this.updateTutorPersonalInformationCommandHandler = updateTutorPersonalInformationCommandHandler;
        this.updateTutorAddressCommandHandler = updateTutorAddressCommandHandler;
        this.updateTutorBankAccountCommandHandler = updateTutorBankAccountCommandHandler;
        this.uploadVerificationDocumentsCommandHandler = uploadVerificationDocumentsCommandHandler;
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
        var uploadVerificationDocumentsCommand = new UploadVerificationDocumentsCommand(
            tutorId,
            identityDocumentFront.OpenReadStream(),
            identityDocumentBack.OpenReadStream(),
            addressDocument.OpenReadStream());

        var uploadVerificationDocumentsCommandResult = await uploadVerificationDocumentsCommandHandler.Handle(uploadVerificationDocumentsCommand, cancellationToken);

        return uploadVerificationDocumentsCommandResult.ToActionResult();
    }
}
