import { Center, Grid, Paper } from "@mantine/core";

export const CalendarSideBar: React.FC = () => (
    <Grid columns={1} gutter={0}>
        <Grid.Col span={1}>
            <Paper style={{ height: "103px" }}></Paper>
        </Grid.Col>
        {Array.from(Array(23).keys()).map(index => (
            <Grid.Col key={index} span={1}>
                <Paper style={{ height: "103px" }}>
                    <Center>{index + 1 < 10 ? `0${index + 1}` : index + 1}:00</Center>
                </Paper>
            </Grid.Col>
        ))}
    </Grid>
);
