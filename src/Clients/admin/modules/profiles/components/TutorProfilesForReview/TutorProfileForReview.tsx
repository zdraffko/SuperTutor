import { Avatar, Badge, Box, Button, Group, Modal, Paper, Spoiler, Stack, Text, Textarea } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import useApproveTutorProfile from "modules/profiles/hooks/useApproveTutorProfile";
import useRequestTutorProfileRedaction from "modules/profiles/hooks/useRequestTutorProfileRedaction";
import { TutorProfile } from "modules/profiles/types";
import { useEffect, useState } from "react";
import { Check, X } from "tabler-icons-react";
import { z } from "zod";

const requestRedactionFormSchema = z.object({
    comment: z.string().min(1, "Моля въведи коментар")
});

interface TutorProfileForReviewProps {
    tutorProfile: TutorProfile;
}

const TutorProfileForReview: React.FC<TutorProfileForReviewProps> = ({ tutorProfile }) => {
    const { approveTutorProfile, isApproveTutorProfileFailed, isApproveTutorProfileLoading, isApproveTutorProfileSuccessful, resetApproveTutorProfileRequestState, approveTutorProfileErrorMessage } =
        useApproveTutorProfile();

    const {
        requestTutorProfileRedaction,
        isRequestTutorProfileRedactionFailed,
        isRequestTutorProfileRedactionLoading,
        isRequestTutorProfileRedactionSuccessful,
        requestTutorProfileRedactionErrorMessage,
        resetRequestTutorProfileRedactionRequestState
    } = useRequestTutorProfileRedaction();

    const [isRedactionRequested, setIsRedactionRequested] = useState(false);

    const requestRedactionForm = useForm({
        schema: zodResolver(requestRedactionFormSchema),
        initialValues: {
            comment: ""
        }
    });

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

    useEffect(() => {
        if (isRequestTutorProfileRedactionSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Редакция за профилът бе поискана успешно",
                color: "teal",
                icon: <Check />
            });

            resetRequestTutorProfileRedactionRequestState();

            return;
        }

        if (isRequestTutorProfileRedactionFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при поискването на редакция за профила",
                message: requestTutorProfileRedactionErrorMessage,
                color: "red",
                icon: <X />
            });

            resetRequestTutorProfileRedactionRequestState();
        }
    }, [isRequestTutorProfileRedactionFailed, isRequestTutorProfileRedactionSuccessful, requestTutorProfileRedactionErrorMessage, resetRequestTutorProfileRedactionRequestState]);

    return (
        <>
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
                        <Button onClick={() => setIsRedactionRequested(true)}>Поискай Редакция</Button>
                    </Stack>
                </Group>
            </Paper>
            <Modal opened={isRedactionRequested} onClose={() => setIsRedactionRequested(false)} title="Поискай Редакция">
                <form
                    onSubmit={requestRedactionForm.onSubmit(async values => {
                        await requestTutorProfileRedaction({ tutorProfileId: tutorProfile.id, comment: values.comment });
                        setIsRedactionRequested(false);
                    })}
                >
                    <Textarea
                        p="sm"
                        label="Причина"
                        required
                        placeholder="Причина за поисканата редакция"
                        {...requestRedactionForm.getInputProps("comment")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи причина")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box p="sm" mt="xl">
                        <Button type="submit" fullWidth size="lg" loading={isRequestTutorProfileRedactionLoading}>
                            Поискай Редакция
                        </Button>
                    </Box>
                </form>
            </Modal>
        </>
    );
};

export default TutorProfileForReview;
