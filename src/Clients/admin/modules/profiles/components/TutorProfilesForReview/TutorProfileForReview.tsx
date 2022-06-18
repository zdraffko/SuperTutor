import { Avatar, Badge, Button, Group, Paper, Spoiler, Stack, Text } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useApproveTutorProfile from "modules/profiles/hooks/useApproveTutorProfile";
import { TutorProfile } from "modules/profiles/types";
import { useEffect } from "react";
import { Check, X } from "tabler-icons-react";

interface TutorProfileForReviewProps {
    tutorProfile: TutorProfile;
}

const TutorProfileForReview: React.FC<TutorProfileForReviewProps> = ({ tutorProfile }) => {
    const { approveTutorProfile, isApproveTutorProfileFailed, isApproveTutorProfileLoading, isApproveTutorProfileSuccessful, resetApproveTutorProfileRequestState, approveTutorProfileErrorMessage } =
        useApproveTutorProfile();

    useEffect(() => {
        if (isApproveTutorProfileSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Профилът бе одобрен успешно",
                color: "teal",
                icon: <Check />
            });

            resetApproveTutorProfileRequestState();

            return;
        }

        if (isApproveTutorProfileFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при одобряването на профила",
                message: approveTutorProfileErrorMessage,
                color: "red",
                icon: <X />
            });

            resetApproveTutorProfileRequestState();
        }
    }, [approveTutorProfileErrorMessage, isApproveTutorProfileFailed, isApproveTutorProfileSuccessful, resetApproveTutorProfileRequestState]);

    return (
        <Paper m="xl" p="xl">
            <Group position="apart">
                <Group>
                    <Avatar radius="lg" size="xl" />
                    <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                        <Group>
                            <Text>Предмет:</Text>
                            <Text>{tutorProfile.tutoringSubject}</Text>
                        </Group>
                        <Group>
                            <Text>Класове:</Text>
                            {tutorProfile.tutoringGrades.map(tutoringGradeName => (
                                <Badge key={`${tutorProfile.id}-${tutoringGradeName}`}>{tutoringGradeName}</Badge>
                            ))}
                        </Group>
                        <Group>
                            <Text>Цена за час:</Text>
                            <Text>{tutorProfile.rateForOneHour} лв.</Text>
                        </Group>
                        <Group>
                            <Text>Описание:</Text>
                            <Spoiler maxHeight={50} showLabel="Show more" hideLabel="Hide">
                                <Text>{tutorProfile.about}</Text>
                            </Spoiler>
                        </Group>
                    </Stack>
                </Group>
                <Stack>
                    <Button loading={isApproveTutorProfileLoading} onClick={async () => await approveTutorProfile({ tutorProfileId: tutorProfile.id })}>
                        Одобри
                    </Button>
                </Stack>
            </Group>
        </Paper>
    );
};

export default TutorProfileForReview;
