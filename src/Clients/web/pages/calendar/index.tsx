import { Grid, Paper } from "@mantine/core";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { CalendarBody, CalendarHeader, CalendarSideBar } from "modules/calendar";
import { NextPage } from "next";
import { DayJs, dayJsRange } from "utils/dates";

const CalendarPage: NextPage = () => {
    const selectedDateRange = dayJsRange(DayJs(), DayJs().add(5, "days"));

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <Paper m="xs">
                    <Grid columns={8} gutter={0}>
                        <Grid.Col span={8} style={{ position: "sticky", top: "0" }}>
                            <CalendarHeader selectedDateRange={selectedDateRange} />
                        </Grid.Col>
                        <Grid.Col span={1}>
                            <CalendarSideBar />
                        </Grid.Col>
                        <Grid.Col span={7}>
                            <CalendarBody selectedDateRange={selectedDateRange} />
                        </Grid.Col>
                    </Grid>
                </Paper>
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default CalendarPage;
