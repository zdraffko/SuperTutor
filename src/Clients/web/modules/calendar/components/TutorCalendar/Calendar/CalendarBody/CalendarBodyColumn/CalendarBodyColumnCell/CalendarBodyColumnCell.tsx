import { Divider, Grid, Paper } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode, TimeSlot } from "modules/calendar/types";
import { DayJs } from "utils/dates";
import CalendarBodyColumnCellHalf from "./CalendarBodyColumnCellHalf";

interface CalendarBodyColumnCellProps {
    date: Dayjs;
    hour: number;
    selectedRedactionMode: CalendarRedactionMode;
    timeSlotsForHour: TimeSlot[];
}

const CalendarBodyColumnCell: React.FC<CalendarBodyColumnCellProps> = ({ date, hour, selectedRedactionMode, timeSlotsForHour }) => (
    <Grid.Col span={1}>
        <Paper style={{ height: "101px" }} onClick={() => console.log(timeSlotsForHour)}>
            <CalendarBodyColumnCellHalf
                date={date}
                hour={hour}
                minute={0}
                selectedRedactionMode={selectedRedactionMode}
                timeSlot={timeSlotsForHour.find(timeSlot => timeSlot.startTime === DayJs().hour(hour).minute(0).format("HH:mm:00"))}
            />
            <Divider variant="dashed" size="sm" />
            <CalendarBodyColumnCellHalf
                date={date}
                hour={hour}
                minute={30}
                selectedRedactionMode={selectedRedactionMode}
                timeSlot={timeSlotsForHour.find(timeSlot => timeSlot.startTime === DayJs().hour(hour).minute(30).format("HH:mm:00"))}
            />
        </Paper>
        <Divider size="sm" />
    </Grid.Col>
);

export default CalendarBodyColumnCell;
