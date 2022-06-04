import { Grid, Paper, Stack, Text } from "@mantine/core";
import { Dayjs } from "dayjs";

interface CalendarHeaderProps {
    selectedDateRange: Dayjs[];
}

const CalendarHeader: React.FC<CalendarHeaderProps> = ({ selectedDateRange }) => (
    <Grid columns={8} gutter={0}>
        <Grid.Col span={1}>
            <Paper style={{ height: "100%" }}></Paper>
        </Grid.Col>
        {selectedDateRange.map(date => (
            <Grid.Col span={1} key={`CalendarHeader-${date.date()}`}>
                <Paper pt="xs" pb="lg">
                    <Stack spacing="xs" align="center" justify="center">
                        <Text size="xs">{date.format("dddd")}</Text>
                        <Text size="xl" weight="bolder">
                            {date.date()}
                        </Text>
                    </Stack>
                </Paper>
            </Grid.Col>
        ))}
    </Grid>
);

export default CalendarHeader;
