import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode, TimeSlot } from "modules/calendar/types";
import { DayJs } from "utils/dates";
import CalendarBodyColumnCell from "./CalendarBodyColumnCell/CalendarBodyColumnCell";

interface CalendarBodyColumn {
    date: Dayjs;
    selectedRedactionMode: CalendarRedactionMode;
    timeSlotsForDate: TimeSlot[];
}

const CalendarBodyColumn: React.FC<CalendarBodyColumn> = ({ date, selectedRedactionMode, timeSlotsForDate }) => (
    <>
        <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        <Grid.Col span={1}>
            <Grid columns={1} gutter={0} onClick={() => console.log(timeSlotsForDate)}>
                {Array.from(Array(24).keys()).map(hour => (
                    <CalendarBodyColumnCell
                        key={`CalendarBodyColumnCell-${date.date()}-${hour}`}
                        date={date}
                        hour={hour}
                        selectedRedactionMode={selectedRedactionMode}
                        timeSlotsForHour={timeSlotsForDate.filter(
                            timeSlot => timeSlot.startTime === DayJs().hour(hour).minute(0).format("HH:mm:00") || timeSlot.startTime === DayJs().hour(hour).minute(30).format("HH:mm:00")
                        )}
                    />
                ))}
            </Grid>
        </Grid.Col>
    </>
);
export default CalendarBodyColumn;
