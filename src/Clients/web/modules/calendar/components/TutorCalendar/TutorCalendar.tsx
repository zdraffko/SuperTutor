import { CalendarRedactionMode } from "modules/calendar/types";
import { useState } from "react";
import { DayJs, dayJsRange } from "utils/dates";
import { Calendar } from "./Calendar/Calendar";
import { CalendarToolbar } from "./CalendarToolbar";

export const TutorCalendar: React.FC = () => {
    const selectedDateRange = dayJsRange(DayJs(), DayJs().add(5, "days"));
    const [selectedRedactionMode, setSelectedRedactionMode] = useState<CalendarRedactionMode>("AddAvailability");

    return (
        <>
            <CalendarToolbar selectedRedactionMode={selectedRedactionMode} setSelectedRedactionMode={setSelectedRedactionMode} />
            <Calendar selectedDateRange={selectedDateRange} selectedRedactionMode={selectedRedactionMode} />
        </>
    );
};
