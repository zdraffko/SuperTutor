using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonReserved;

internal class AssignAvailabilityTimeSlotsDomainEventHandler : IDomainEventHandler<LessonReservedDomainEvent>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;

    public AssignAvailabilityTimeSlotsDomainEventHandler(
        ITimeSlotQueryModelRepository timeSlotQueryModelRepository,
        IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository)
    {
        this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;
        this.timeSlotRepository = timeSlotRepository;
    }

    public async Task Handle(LessonReservedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lessonStart = domainEvent.Date.ToDateTime(domainEvent.StartTime);
        var lessonEnd = lessonStart.Add(domainEvent.Duration);

        var timeSlotIdsForLesson = await timeSlotQueryModelRepository.GetIdsForLesson(domainEvent.TutorId, lessonStart, lessonEnd, cancellationToken);

        foreach (var timeSlotId in timeSlotIdsForLesson)
        {
            var timeSlot = await timeSlotRepository.Load(timeSlotId, cancellationToken);
            if (timeSlot is null)
            {
                continue;
            }

            timeSlot.AssignAvailability();

            await timeSlotRepository.Update(timeSlot, cancellationToken);
        }
    }
}
