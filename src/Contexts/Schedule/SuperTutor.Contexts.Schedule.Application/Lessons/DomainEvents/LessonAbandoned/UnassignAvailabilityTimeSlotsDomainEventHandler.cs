using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Events;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.DomainEvents.LessonAbandoned;

internal class UnassignAvailabilityTimeSlotsDomainEventHandler : IDomainEventHandler<LessonAbandonedDomainEvent>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;
    private readonly IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository;

    public UnassignAvailabilityTimeSlotsDomainEventHandler(
        ITimeSlotQueryModelRepository timeSlotQueryModelRepository,
        IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository,
        IAggregateRootEventsRepository<Lesson, LessonId, Guid> lessonRepository)
    {
        this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;
        this.timeSlotRepository = timeSlotRepository;
        this.lessonRepository = lessonRepository;
    }

    public async Task Handle(LessonAbandonedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.Load(domainEvent.LessonId, cancellationToken);
        if (lesson is null)
        {
            return;
        }

        var lessonStart = lesson.Date.ToDateTime(lesson.StartTime);
        var lessonEnd = lessonStart.Add(lesson.Duration);

        var timeSlotIdsForLesson = await timeSlotQueryModelRepository.GetIdsForLesson(lesson.TutorId, lessonStart, lessonEnd, cancellationToken);

        foreach (var timeSlotId in timeSlotIdsForLesson)
        {
            var timeSlot = await timeSlotRepository.Load(timeSlotId, cancellationToken);
            if (timeSlot is null)
            {
                continue;
            }

            timeSlot.UnassignAvailability();

            await timeSlotRepository.Update(timeSlot, cancellationToken);
        }
    }
}
