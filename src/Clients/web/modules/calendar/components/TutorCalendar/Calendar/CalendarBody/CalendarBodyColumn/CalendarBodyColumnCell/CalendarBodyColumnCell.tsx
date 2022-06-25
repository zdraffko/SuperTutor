import { Divider, Grid, Paper } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode, Lesson, TimeSlot } from "modules/calendar/types";
import { DayJs } from "utils/dates";
import CalendarBodyColumnCellHalf from "./CalendarBodyColumnCellHalf";

interface CalendarBodyColumnCellProps {
    date: Dayjs;
    hour: number;
    selectedRedactionMode: CalendarRedactionMode;
    timeSlotsForHour: TimeSlot[];
    scheduledLessonForHour: Lesson | undefined;
}

const CalendarBodyColumnCell: React.FC<CalendarBodyColumnCellProps> = ({ date, hour, selectedRedactionMode, timeSlotsForHour, scheduledLessonForHour }) => {
    const scheduledLessonForUpperCell =
        scheduledLessonForHour?.startTime === DayJs().hour(hour).minute(0).format("HH:mm:00") ||
        scheduledLessonForHour?.startTime ===
            DayJs()
                .hour(hour - 1)
                .minute(30)
                .format("HH:mm:00")
            ? scheduledLessonForHour
            : undefined;

    const scheduledLessonForLowerCell =
        scheduledLessonForHour?.startTime === DayJs().hour(hour).minute(30).format("HH:mm:00") || scheduledLessonForHour?.startTime === DayJs().hour(hour).minute(0).format("HH:mm:00")
            ? scheduledLessonForHour
            : undefined;

    return (
        <Grid.Col span={1}>
            <Paper style={{ height: "101px" }}>
                <CalendarBodyColumnCellHalf
                    date={date}
                    hour={hour}
                    minute={0}
                    selectedRedactionMode={selectedRedactionMode}
                    timeSlot={timeSlotsForHour.find(timeSlot => timeSlot.startTime === DayJs().hour(hour).minute(0).format("HH:mm:00"))}
                    scheduledLesson={scheduledLessonForUpperCell}
                />
                {(!scheduledLessonForLowerCell || !scheduledLessonForUpperCell) && <Divider variant="dashed" size="sm" />}
                <CalendarBodyColumnCellHalf
                    date={date}
                    hour={hour}
                    minute={30}
                    selectedRedactionMode={selectedRedactionMode}
                    timeSlot={timeSlotsForHour.find(timeSlot => timeSlot.startTime === DayJs().hour(hour).minute(30).format("HH:mm:00"))}
                    scheduledLesson={scheduledLessonForLowerCell}
                />
            </Paper>
            {!scheduledLessonForLowerCell && <Divider size="sm" />}
        </Grid.Col>
    );
};

export default CalendarBodyColumnCell;
