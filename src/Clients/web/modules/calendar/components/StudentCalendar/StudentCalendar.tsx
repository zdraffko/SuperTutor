import { Center, Loader, Paper, Stack, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useGetScheduledLessonsForStudent from "modules/calendar/hooks/useGetScheduledLessonsForStudent";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { ScheduledLessons } from "./ScheduledLessons";

export const StudentCalendar: React.FC = () => {
    const { scheduledLessonsForStudent, isGetScheduledLessonsForStudentFailed, isGetScheduledLessonsForStudentLoading, getScheduledLessonsForStudentErrorMessage } = useGetScheduledLessonsForStudent();

    useEffect(() => {
        if (isGetScheduledLessonsForStudentFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при вземането на предстоящите уроци",
                message: getScheduledLessonsForStudentErrorMessage,
                color: "red",
                icon: <X />
            });

            return;
        }
    }, [getScheduledLessonsForStudentErrorMessage, isGetScheduledLessonsForStudentFailed]);

    if (isGetScheduledLessonsForStudentLoading || !scheduledLessonsForStudent) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <Paper p="xl" m="xl">
            <Stack>
                <Title align="center">Моите уроци</Title>
                {scheduledLessonsForStudent.length == 0 ? <Title order={4}>Нямате уроци за момента</Title> : <ScheduledLessons scheduledLessons={scheduledLessonsForStudent} />}
            </Stack>
        </Paper>
    );
};
