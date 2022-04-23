using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.StudentProfiles.Commands.DeleteStudent;
internal class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository studentRepository;

    public DeleteStudentCommandHandler(IStudentRepository studentRepository) => this.studentRepository = studentRepository;

    public Task<Result> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
    {
        studentRepository.DeleteById(command.StudentId, cancellationToken);

        return Task.FromResult(Result.Ok());
    }
}
