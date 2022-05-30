export type CalendarRedactionMode = "AddAvailability" | "TakeTimeOff" | "RemoveTimeSlot";

export type TimeSlotType = "Availability" | "TimeOff";

export type TimeSlotStatus = "Assigned" | "Unassigned" | "Removed";

export interface TimeSlot {
    id: string;
    tutorId: string;
    date: string;
    startTime: string;
    type: TimeSlotType;
    status: TimeSlotStatus;
}
