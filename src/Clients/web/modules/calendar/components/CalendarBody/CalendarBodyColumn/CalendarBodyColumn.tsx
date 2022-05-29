import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import CalendarBodyColumnCell from "./CalendarBodyColumnCell/CalendarBodyColumnCell";

interface CalendarBodyColumn {
    date: Dayjs;
}

const CalendarBodyColumn: React.FC<CalendarBodyColumn> = ({ date }) => (
    <>
        <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        <Grid.Col span={1}>
            <Grid columns={1} gutter={0}>
                {Array.from(Array(24).keys()).map(hour => (
                    <CalendarBodyColumnCell key={`CalendarBodyColumnCell-${date.date()}-${hour}`} date={date} hour={hour} />
                ))}
            </Grid>
        </Grid.Col>
    </>
);

export default CalendarBodyColumn;
