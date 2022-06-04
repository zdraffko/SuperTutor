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

export interface Lesson {
    id: string;
    tutorId: string;
    studentId: string;
    date: string;
    startTime: string;
    duration: string;
    subject: string;
    grade: string;
    type: string;
    status: string;
    paymentStatus: string;
}
