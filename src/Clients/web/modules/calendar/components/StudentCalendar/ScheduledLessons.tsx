import { Divider, Group, Stack, Text } from "@mantine/core";
import { Lesson } from "modules/calendar/types";

interface ScheduledLessonsProps {
    scheduledLessons: Lesson[];
}

export const ScheduledLessons: React.FC<ScheduledLessonsProps> = ({ scheduledLessons }) => (
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
                </Group>
                <Divider style={{ width: "100%" }} />
            </>
        ))}
    </>
);
