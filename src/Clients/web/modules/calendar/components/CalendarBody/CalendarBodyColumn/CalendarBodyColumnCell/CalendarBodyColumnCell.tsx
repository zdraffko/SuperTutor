import { Divider, Grid, Paper } from "@mantine/core";
import { Dayjs } from "dayjs";
import CalendarBodyColumnCellHalf from "./CalendarBodyColumnCellHalf";

interface CalendarBodyColumnCellProps {
    date: Dayjs;
    hour: number;
}

const CalendarBodyColumnCell: React.FC<CalendarBodyColumnCellProps> = ({ date, hour }) => (
    <Grid.Col span={1}>
        <Paper style={{ height: "101px" }}>
            <CalendarBodyColumnCellHalf date={date} hour={hour} minute={0} />
            <Divider variant="dashed" size="sm" />
            <CalendarBodyColumnCellHalf date={date} hour={hour} minute={30} />
        </Paper>
        <Divider size="sm" />
    </Grid.Col>
);

export default CalendarBodyColumnCell;
