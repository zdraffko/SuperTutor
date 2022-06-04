using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonReserved;

internal class CreateLessonQueryModelDomainEventHandler : IDomainEventHandler<LessonReservedDomainEvent>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public CreateLessonQueryModelDomainEventHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task Handle(LessonReservedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lessonQueryModel = new LessonQueryModel
        {
            Id = domainEvent.LessonId,
            TutorId = domainEvent.TutorId,
            StudentId = domainEvent.StudentId,
            Date = domainEvent.Date.ToDateTime(domainEvent.StartTime),
            StartTime = domainEvent.StartTime.ToTimeSpan(),
            Duration = domainEvent.Duration,
            Subject = domainEvent.Subject,
            Grade = domainEvent.Grade,
            Type = domainEvent.Type.Name,
            Status = domainEvent.Status.Name,
            PaymentStatus = domainEvent.PaymentStatus.Name
        };

        await lessonQueryModelRepository.Create(lessonQueryModel, cancellationToken);
    }
}
