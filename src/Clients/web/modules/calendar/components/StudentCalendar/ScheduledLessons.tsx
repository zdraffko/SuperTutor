import { Button, Divider, Group, Stack, Text } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useCompleteLesson from "modules/calendar/hooks/useCompleteLesson";
import { Lesson } from "modules/calendar/types";
import { useEffect } from "react";
import { Check, X } from "tabler-icons-react";

interface ScheduledLessonsProps {
    scheduledLessons: Lesson[];
}

export const ScheduledLessons: React.FC<ScheduledLessonsProps> = ({ scheduledLessons }) => {
    const { completeLesson, isCompleteLessonFailed, isCompleteLessonLoading, isCompleteLessonSuccessful, completeLessonErrorMessage, resetCompleteLessonRequestState } = useCompleteLesson();

    useEffect(() => {
        if (isCompleteLessonFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при потвърждаването на урока",
                message: completeLessonErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        if (isCompleteLessonSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Успешно потвърждаване на урока",
                color: "teal",
                icon: <Check />
            });
        }

        resetCompleteLessonRequestState();
    }, [completeLessonErrorMessage, isCompleteLessonFailed, isCompleteLessonSuccessful, resetCompleteLessonRequestState]);

    return (
        <>
            {scheduledLessons.map(scheduledLesson => (
                <>
                    <Group key={scheduledLesson.id} mt="xl" grow pl="xl">
                        <Stack>
                            <Group>
                                <Text weight="bold">Учител: </Text>
                                <Text>Тест Учител</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Предмет: </Text>
                                <Text>{scheduledLesson.subject}</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Клас: </Text>
                                <Text>{scheduledLesson.grade}</Text>
                            </Group>
                        </Stack>
                        <Stack>
                            <Group>
                                <Text weight="bold">Дата: </Text>
                                <Text>{scheduledLesson.date}</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Час: </Text>
                                <Text>{scheduledLesson.startTime}</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Продължителност: </Text>
                                <Text>{scheduledLesson.duration}</Text>
                            </Group>
                        </Stack>
                        <Stack>
                            <Group>
                                <Text weight="bold">Тип на урока: </Text>
                                <Text>{scheduledLesson.type}</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Статус: </Text>
                                <Text>{scheduledLesson.status}</Text>
                            </Group>
                            <Group>
                                <Text weight="bold">Статус на плащането: </Text>
                                <Text>{scheduledLesson.paymentStatus}</Text>
                            </Group>
                        </Stack>
                        {scheduledLesson.status === "Приключил" && (
                            <Button onClick={async () => await completeLesson({ lessonId: scheduledLesson.id })} loading={isCompleteLessonLoading}>
                                Потвърди урокът
                            </Button>
                        )}
                    </Group>
                    <Divider style={{ width: "100%" }} />
                </>
            ))}
        </>
    );
};
