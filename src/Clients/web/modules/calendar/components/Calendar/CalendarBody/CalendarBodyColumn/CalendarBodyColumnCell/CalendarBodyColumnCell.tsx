import { Divider, Grid, Paper } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode } from "modules/calendar/types";
import CalendarBodyColumnCellHalf from "./CalendarBodyColumnCellHalf";

interface CalendarBodyColumnCellProps {
    date: Dayjs;
    hour: number;
    selectedRedactionMode: CalendarRedactionMode;
}

const CalendarBodyColumnCell: React.FC<CalendarBodyColumnCellProps> = ({ date, hour, selectedRedactionMode }) => (
    <Grid.Col span={1}>
        <Paper style={{ height: "101px" }}>
            <CalendarBodyColumnCellHalf date={date} hour={hour} minute={0} selectedRedactionMode={selectedRedactionMode} />
            <Divider variant="dashed" size="sm" />
            <CalendarBodyColumnCellHalf date={date} hour={hour} minute={30} selectedRedactionMode={selectedRedactionMode} />
        </Paper>
        <Divider size="sm" />
    </Grid.Col>
);

export default CalendarBodyColumnCell;
