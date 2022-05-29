import { Divider, Grid } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode } from "modules/calendar/types";
import CalendarBodyColumn from "./CalendarBodyColumn/CalendarBodyColumn";

interface CalendarBodyProps {
    selectedDateRange: Dayjs[];
    selectedRedactionMode: CalendarRedactionMode;
}

const CalendarBody: React.FC<CalendarBodyProps> = ({ selectedDateRange, selectedRedactionMode }) => (
    <>
        <Divider size="sm" />
        <Grid columns={8} gutter={0} grow>
            {selectedDateRange.map(date => (
                <CalendarBodyColumn key={`CalendarBodyColumn-${date.date()}`} date={date} selectedRedactionMode={selectedRedactionMode} />
            ))}
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        </Grid>
    </>
);

export default CalendarBody;
