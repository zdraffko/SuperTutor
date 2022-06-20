import { Avatar, Badge, Box, Button, Group, Modal, Paper, Spoiler, Stack, Text, Textarea, Title } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import { useEffect, useState } from "react";
import { Check, X } from "tabler-icons-react";
import { tutoringGrades } from "types/tutoringGrades";
import { tutoringSubjects } from "types/tutoringSubjects";
import { z } from "zod";
import useSubmitTutorProfileForReview from "../../hooks/useSubmitTutorProfileForReview";
import useUpdateTutorProfileAbout from "../../hooks/useUpdateTutorProfileAbout";
import { TutorProfile } from "../../types/tutorProfile";

const updateAboutFormSchema = z.object({
    newAbout: z.string().min(1, "Моля въведи описание")
});

interface TutorProfilesListItemProps {
    tutorProfile: TutorProfile;
}

const TutorProfilesListItem: React.FC<TutorProfilesListItemProps> = ({ tutorProfile }) => {
    const tutoringSubjectName = tutoringSubjects.find(tutoringSubject => tutoringSubject.value === tutorProfile.tutoringSubject)?.name;
    const tutoringGradeNames = tutorProfile.tutoringGrades.map(tutoringGradeValue => tutoringGrades.find(tutoringGrade => tutoringGrade.value === tutoringGradeValue)?.name);
    const isProfileForRedaction = tutorProfile.status === "ForRedaction";
    const [isEditModalOpened, setIsEditModalOpened] = useState(false);

    const updateAboutForm = useForm({
        schema: zodResolver(updateAboutFormSchema),
        initialValues: {
            newAbout: ""
        }
    });

    const {
        updateTutorProfileAbout,
        isUpdateTutorProfileAboutFailed,
        isUpdateTutorProfileAboutLoading,
        isUpdateTutorProfileAboutSuccessful,
        updateTutorProfileAboutErrorMessage,
        resetUpdateTutorProfileAboutRequestState
    } = useUpdateTutorProfileAbout();

    const {
        submitTutorProfileForReview,
        isSubmitTutorProfileForReviewFailed,
        isSubmitTutorProfileForReviewLoading,
        isSubmitTutorProfileForReviewSuccessful,
        submitTutorProfileForReviewErrorMessage,
        resetSubmitTutorProfileForReviewRequestState
    } = useSubmitTutorProfileForReview();

    useEffect(() => {
        if (isUpdateTutorProfileAboutSuccessful) {
            submitTutorProfileForReview({ tutorProfileId: tutorProfile.id }).then(() => {
                setIsEditModalOpened(false);
                resetUpdateTutorProfileAboutRequestState();
            });
        }

        if (isSubmitTutorProfileForReviewSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Профилът бе редактирен успешно",
                color: "teal",
                icon: <Check />
            });

            resetSubmitTutorProfileForReviewRequestState();

            return;
        }

        if (isUpdateTutorProfileAboutFailed || isSubmitTutorProfileForReviewFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при редактирането на профила",
                message: updateTutorProfileAboutErrorMessage && submitTutorProfileForReviewErrorMessage,
                color: "red",
                icon: <X />
            });

            resetUpdateTutorProfileAboutRequestState();
            resetSubmitTutorProfileForReviewRequestState();
        }
    }, [
        isSubmitTutorProfileForReviewFailed,
        isSubmitTutorProfileForReviewSuccessful,
        isUpdateTutorProfileAboutFailed,
        isUpdateTutorProfileAboutSuccessful,
        resetSubmitTutorProfileForReviewRequestState,
        resetUpdateTutorProfileAboutRequestState,
        submitTutorProfileForReview,
        submitTutorProfileForReviewErrorMessage,
        tutorProfile.id,
        updateTutorProfileAboutErrorMessage
    ]);

    return (
        <>
            <Paper m="xl" p="xl" withBorder={isProfileForRedaction} style={{ borderColor: "red" }}>
                <Group position="apart">
                    <Group>
                        <Avatar radius="lg" size="xl" />
                        <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                            <Group>
                                <Text>Предмет:</Text>
                                <Text>{tutoringSubjectName}</Text>
                            </Group>
                            <Group>
                                <Text>Класове:</Text>
                                {tutoringGradeNames.map(tutoringGradeName => (
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
                        {isProfileForRedaction && <Button onClick={() => setIsEditModalOpened(true)}>Редактирай</Button>}
                        <Button color="red">Изтрий</Button>
                    </Stack>
                </Group>
            </Paper>
            <Modal opened={isEditModalOpened} onClose={() => setIsEditModalOpened(false)} title="Редактирай Профила">
                <Title order={4}>Коментар от администратор:</Title>
                <Text>{tutorProfile.redactionComment}</Text>
                <form
                    onSubmit={updateAboutForm.onSubmit(async values => {
                        await updateTutorProfileAbout({ tutorProfileId: tutorProfile.id, newAbout: values.newAbout });
                    })}
                >
                    <Textarea
                        mt="xl"
                        label="Ново Описание"
                        required
                        placeholder="Ново описание, което адресира проблемите описани в коментара"
                        {...updateAboutForm.getInputProps("newAbout")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи ново описание")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box mt="xl">
                        <Button type="submit" fullWidth size="lg" loading={isUpdateTutorProfileAboutLoading || isSubmitTutorProfileForReviewLoading}>
                            Редактирай Профила
                        </Button>
                    </Box>
                </form>
            </Modal>
        </>
    );
};

export default TutorProfilesListItem;
