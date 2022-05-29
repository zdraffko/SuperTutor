import { Grid, Paper } from "@mantine/core";
import { Dayjs } from "dayjs";
import { CalendarRedactionMode } from "modules/calendar/types";
import CalendarBody from "./CalendarBody/CalendarBody";
import CalendarHeader from "./CalendarHeader";
import CalendarSideBar from "./CalendarSideBar";

interface CalendarProps {
    selectedDateRange: Dayjs[];
    selectedRedactionMode: CalendarRedactionMode;
}

export const Calendar: React.FC<CalendarProps> = ({ selectedDateRange, selectedRedactionMode }) => (
    <Paper m="xs">
        <Grid columns={8} gutter={0}>
            <Grid.Col span={8} style={{ position: "sticky", top: "0" }}>
                <CalendarHeader selectedDateRange={selectedDateRange} />
            </Grid.Col>
            <Grid.Col span={1}>
                <CalendarSideBar />
            </Grid.Col>
            <Grid.Col span={7}>
                <CalendarBody selectedDateRange={selectedDateRange} selectedRedactionMode={selectedRedactionMode} />
            </Grid.Col>
        </Grid>
    </Paper>
);
