import { Center, Container, Loader, Text, useMantineTheme } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { Dayjs } from "dayjs";
import { CellColors, RedactionModeColors } from "modules/calendar/constants";
import useAddAvailability from "modules/calendar/hooks/useAddAvailability";
import { CalendarRedactionMode, Lesson, TimeSlot } from "modules/calendar/types";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { DayJs } from "utils/dates";

interface CalendarBodyColumnCellHalfProps {
    date: Dayjs;
    hour: number;
    minute: number;
    selectedRedactionMode: CalendarRedactionMode;
    timeSlot: TimeSlot | undefined;
    scheduledLesson: Lesson | undefined;
}

const CalendarBodyColumnCellHalf: React.FC<CalendarBodyColumnCellHalfProps> = ({ date, hour, minute, selectedRedactionMode, timeSlot, scheduledLesson }) => {
    const formattedDate = date.format("DD/MM/YYYY");
    const formattedTime = DayJs().hour(hour).minute(minute).format("HH:mm:00");
    const { addAvailability, isAddAvailabilityFailed, isAddAvailabilityLoading, addAvailabilityErrorMessage, resetAddAvailabilityRequestState } = useAddAvailability();
    const theme = useMantineTheme();

    useEffect(() => {
        if (isAddAvailabilityFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при добавянето на наличност",
                message: addAvailabilityErrorMessage,
                color: "red",
                icon: <X />
            });

            resetAddAvailabilityRequestState();

            return;
        }
    }, [addAvailabilityErrorMessage, isAddAvailabilityFailed, resetAddAvailabilityRequestState]);

    const handleOnClick = async () => {
        if (selectedRedactionMode === "AddAvailability") {
            await addAvailability({ date: formattedDate, startTime: formattedTime });
        }
    };

    if (isAddAvailabilityLoading) {
        return (
            <Container style={{ height: "50px" }} sx={theme => ({ backgroundColor: theme.colors[RedactionModeColors[selectedRedactionMode]][3] })}>
                <Center style={{ height: "100%" }}>
                    <Loader size="sm" />
                </Center>
            </Container>
        );
    }

    let scheduledLessonColor = theme.colors.teal[5];

    if (scheduledLesson?.status === "Започнал") {
        scheduledLessonColor = theme.colors.orange[5];
    }

    if (scheduledLesson?.status === "Приключил") {
        scheduledLessonColor = theme.colors.red[5];
    }

    if (scheduledLesson?.status === "Завършен") {
        scheduledLessonColor = theme.colors.gray[5];
    }

    return timeSlot ? (
        <Container
            style={{ height: "50px", padding: "1px" }}
            sx={theme => ({
                backgroundColor: scheduledLesson ? scheduledLessonColor : theme.colors[CellColors[timeSlot.type]][2]
            })}
        >
            {scheduledLesson && scheduledLesson.startTime === timeSlot.startTime && (
                <>
                    <Text size="xs">Предмет: {scheduledLesson?.subject}</Text>
                    <Text size="xs">Клас: {scheduledLesson?.grade}</Text>
                    <Text size="xs">Статус: {scheduledLesson?.status}</Text>
                </>
            )}
        </Container>
    ) : (
        <Container
            onClick={handleOnClick}
            style={{ height: "50px" }}
            sx={theme => ({
                "&:hover": {
                    cursor: "pointer",
                    backgroundColor: theme.colors[RedactionModeColors[selectedRedactionMode]][4]
                }
            })}
        ></Container>
    );
};
export default CalendarBodyColumnCellHalf;
