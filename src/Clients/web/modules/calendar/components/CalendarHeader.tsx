import { Grid, Paper, Stack, Text } from "@mantine/core";

interface TopRowElement {
    dayOfWeek: string;
    date: number;
    timeSlots: number[];
}

const topTowData: TopRowElement[] = [
    { dayOfWeek: "Неделя", date: 22, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Понеделник", date: 23, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Вторник", date: 24, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Сряда", date: 25, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Четвъртък", date: 26, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Петък", date: 27, timeSlots: Array.from(Array(24).keys()) },
    { dayOfWeek: "Събота", date: 28, timeSlots: Array.from(Array(24).keys()) }
];

export const CalendarHeader: React.FC = () => (
    <Grid columns={8} gutter={0}>
        <Grid.Col span={1}></Grid.Col>
        {topTowData.map(element => (
            <Grid.Col span={1} key={`${element.dayOfWeek}-${element.date}`}>
                <Paper pt="xs" pb="lg">
                    <Stack spacing="xs" align="center" justify="center">
                        <Text size="xs">{element.dayOfWeek}</Text>
                        <Text size="xl" weight="bolder">
                            {element.date}
                        </Text>
                    </Stack>
                </Paper>
            </Grid.Col>
        ))}
    </Grid>
);
