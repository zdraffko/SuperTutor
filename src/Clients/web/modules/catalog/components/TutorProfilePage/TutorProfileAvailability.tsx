import { Group, Stack, Text, Title, useMantineTheme } from "@mantine/core";
import { CatalogTutorProfile, TutorAvailability } from "modules/catalog/types";
import { useState } from "react";
import { TrialLessonReservationModal } from "./TrialLessonReservationModal";

interface TutorProfileAvailabilityProps {
    tutorAvailabilities: TutorAvailability[];
    tutorProfile: CatalogTutorProfile;
}

export const TutorProfileAvailability: React.FC<TutorProfileAvailabilityProps> = ({ tutorAvailabilities, tutorProfile }) => {
    const theme = useMantineTheme();
    const [isReserveTrialLessonModalOpened, setIsReserveTrialLessonModalOpened] = useState(false);

    const [hourState, setHour] = useState("");
    const [dateState, setDate] = useState("");

    return (
        <>
            <Title order={3} align="center">
                Свободни часове
            </Title>
            <Group position="center" align="flex-start">
                {tutorAvailabilities.length === 0 ? (
                    <Text>Учителят няма свободни часове в момента</Text>
                ) : (
                    tutorAvailabilities.map(tutorAvailability => (
                        <Stack key={tutorAvailability.date}>
                            <Title order={5}>{tutorAvailability.date}</Title>
                            {tutorAvailability.hours.map(hour => (
                                <Text
                                    onClick={() => {
                                        setHour(hour);
                                        setDate(tutorAvailability.date);
                                        setIsReserveTrialLessonModalOpened(true);
                                    }}
                                    color={theme.primaryColor}
                                    key={`${tutorAvailability.date}-${hour}`}
                                    sx={() => ({
                                        "&:hover": {
                                            cursor: "pointer"
                                        }
                                    })}
                                >
                                    {hour}
                                </Text>
                            ))}
                        </Stack>
                    ))
                )}
                <TrialLessonReservationModal
                    tutorProfile={tutorProfile}
                    isOpened={isReserveTrialLessonModalOpened}
                    onClose={() => setIsReserveTrialLessonModalOpened(false)}
                    trialLessonDate={dateState}
                    trialLessonHour={hourState}
                />
            </Group>
        </>
    );
};
