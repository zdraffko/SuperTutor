import { Grid, Paper } from "@mantine/core";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { CalendarBody, CalendarHeader, CalendarSideBar } from "modules/calendar";
import { NextPage } from "next";

const CalendarPage: NextPage = () => (
    <AuthenticationProtectedPage>
        <MainLayout>
            <Paper m="xs">
                <Grid columns={8} gutter={0}>
                    <Grid.Col span={8}>
                        <CalendarHeader />
                    </Grid.Col>
                    <Grid.Col span={1}>
                        <CalendarSideBar />
                    </Grid.Col>
                    <Grid.Col span={7}>
                        <CalendarBody />
                    </Grid.Col>
                </Grid>
            </Paper>
        </MainLayout>
    </AuthenticationProtectedPage>
);

export default CalendarPage;
