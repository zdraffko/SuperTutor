import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode } from "modules/calendar/types";
import CalendarBodyColumnCell from "./CalendarBodyColumnCell/CalendarBodyColumnCell";

interface CalendarBodyColumn {
    date: Dayjs;
    selectedRedactionMode: CalendarRedactionMode;
}

const CalendarBodyColumn: React.FC<CalendarBodyColumn> = ({ date, selectedRedactionMode }) => (
    <>
        <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        <Grid.Col span={1}>
            <Grid columns={1} gutter={0}>
                {Array.from(Array(24).keys()).map(hour => (
                    <CalendarBodyColumnCell key={`CalendarBodyColumnCell-${date.date()}-${hour}`} date={date} hour={hour} selectedRedactionMode={selectedRedactionMode} />
                ))}
            </Grid>
        </Grid.Col>
    </>
);

export default CalendarBodyColumn;
