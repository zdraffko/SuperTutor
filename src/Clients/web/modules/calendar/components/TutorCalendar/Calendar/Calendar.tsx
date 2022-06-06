import { Center, Grid, Loader, Paper } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { Dayjs } from "dayjs";
import useGetScheduledLessonsForTutor from "modules/calendar/hooks/useGetScheduledLessonsForTutor";
import useGetTutorTimeSlotsForWeek from "modules/calendar/hooks/useGetTutorTimeSlotsForWeek";
import { CalendarRedactionMode } from "modules/calendar/types";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import CalendarBody from "./CalendarBody/CalendarBody";
import CalendarHeader from "./CalendarHeader";
import CalendarSideBar from "./CalendarSideBar";

interface CalendarProps {
    selectedDateRange: Dayjs[];
    selectedRedactionMode: CalendarRedactionMode;
}

export const Calendar: React.FC<CalendarProps> = ({ selectedDateRange, selectedRedactionMode }) => {
    const { timeSlotsForWeek, isGetTutorTimeSlotsForWeekFailed, isGetTutorTimeSlotsForWeekLoading, getTutorTimeSlotsForWeekErrorMessage } = useGetTutorTimeSlotsForWeek({
        weekStartDate: selectedDateRange[0].format("DD/MM/YYYY")
    });
    const { scheduledLessonsForTutor, getScheduledLessonsForTutorErrorMessage, isGetScheduledLessonsForTutorFailed, isGetScheduledLessonsForTutorLoading } = useGetScheduledLessonsForTutor();

    useEffect(() => {
        if (isGetTutorTimeSlotsForWeekFailed || isGetScheduledLessonsForTutorFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при зареждането на графика",
                message: getTutorTimeSlotsForWeekErrorMessage ?? getScheduledLessonsForTutorErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getScheduledLessonsForTutorErrorMessage, getTutorTimeSlotsForWeekErrorMessage, isGetScheduledLessonsForTutorFailed, isGetTutorTimeSlotsForWeekFailed]);

    if (isGetTutorTimeSlotsForWeekLoading || !timeSlotsForWeek || isGetScheduledLessonsForTutorLoading || !scheduledLessonsForTutor) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <Paper m="xs">
            <Grid columns={8} gutter={0}>
                <Grid.Col span={8} style={{ position: "sticky", top: "0" }}>
                    <CalendarHeader selectedDateRange={selectedDateRange} />
                </Grid.Col>
                <Grid.Col span={1}>
                    <CalendarSideBar />
                </Grid.Col>
                <Grid.Col span={7}>
                    <CalendarBody selectedDateRange={selectedDateRange} selectedRedactionMode={selectedRedactionMode} timeSlotsForWeek={timeSlotsForWeek} scheduledLessons={scheduledLessonsForTutor} />
                </Grid.Col>
            </Grid>
        </Paper>
    );
};
