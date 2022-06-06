import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode, Lesson, TimeSlot } from "modules/calendar/types";
import CalendarBodyColumn from "./CalendarBodyColumn/CalendarBodyColumn";

interface CalendarBodyProps {
    selectedDateRange: Dayjs[];
    selectedRedactionMode: CalendarRedactionMode;
    timeSlotsForWeek: TimeSlot[];
    scheduledLessons: Lesson[];
}

const CalendarBody: React.FC<CalendarBodyProps> = ({ selectedDateRange, selectedRedactionMode, timeSlotsForWeek, scheduledLessons }) => (
    <>
        <Divider size="sm" />
        <Grid columns={8} gutter={0} grow>
            {selectedDateRange.map(date => (
                <CalendarBodyColumn
                    key={`CalendarBodyColumn-${date.date()}`}
                    date={date}
                    selectedRedactionMode={selectedRedactionMode}
                    timeSlotsForDate={timeSlotsForWeek.filter(timeSlot => timeSlot.date === date.format("DD/MM/YYYY"))}
                    scheduledLessonsForDate={scheduledLessons.filter(lesson => lesson.date === date.format("DD/MM/YYYY"))}
                />
            ))}
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        </Grid>
    </>
);

export default CalendarBody;
