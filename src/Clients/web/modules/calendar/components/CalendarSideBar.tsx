import { Center, Grid, Paper } from "@mantine/core";

export const CalendarSideBar: React.FC = () => (
    <Grid columns={1} gutter={0}>
        <Grid.Col span={1}>
            <Paper style={{ height: "103px" }}></Paper>
        </Grid.Col>
        {Array.from(Array(23).keys())
            .map(index => ++index)
            .map(hour => (
                <Grid.Col key={hour} span={1}>
                    <Paper style={{ height: "103px" }}>
                        <Center>{hour < 10 ? `0${hour}` : hour}:00</Center>
                    </Paper>
                </Grid.Col>
            ))}
    </Grid>
);
