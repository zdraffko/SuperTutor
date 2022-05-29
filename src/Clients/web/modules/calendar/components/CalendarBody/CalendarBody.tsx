import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import CalendarBodyColumn from "./CalendarBodyColumn/CalendarBodyColumn";

interface CalendarBodyProps {
    selectedDateRange: Dayjs[];
}

export const CalendarBody: React.FC<CalendarBodyProps> = ({ selectedDateRange }) => (
    <>
        <Divider size="sm" />
        <Grid columns={8} gutter={0} grow>
            {selectedDateRange.map(date => (
                <CalendarBodyColumn key={`CalendarBodyColumn-${date.date()}`} date={date} />
            ))}
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        </Grid>
    </>
);
