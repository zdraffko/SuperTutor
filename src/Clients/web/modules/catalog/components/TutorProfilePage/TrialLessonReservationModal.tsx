import { Button, Center, Group, Modal, Select, Text } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import useCreateCharge from "modules/catalog/hooks/useCreateCharge";
import useReserveTrialLesson from "modules/catalog/hooks/useReserveTrialLesson";
import { CatalogTutorProfile } from "modules/catalog/types";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { Check, X } from "tabler-icons-react";
import { z } from "zod";

const TrialLessonReservationFormSchema = z.object({
    tutoringGrade: z.string().min(1, "Моля избери клас")
});

interface ReserveTrialLessonModalProps {
    isOpened: boolean;
    onClose: () => void;
    tutorProfile: CatalogTutorProfile;
    trialLessonDate: string;
    trialLessonHour: string;
}

export const TrialLessonReservationModal: React.FC<ReserveTrialLessonModalProps> = ({ isOpened, onClose, tutorProfile, trialLessonDate, trialLessonHour }) => {
    const form = useForm({
        schema: zodResolver(TrialLessonReservationFormSchema),
        initialValues: {
            tutoringGrade: ""
        }
    });

    const { reserveTrialLesson, isReserveTrialLessonFailed, isReserveTrialLessonLoading, isReserveTrialLessonSuccessful, reserveTrialLessonErrorMessage, resetReserveTrialLessonRequestState } =
        useReserveTrialLesson();
    const { createCharge } = useCreateCharge();

    useEffect(() => {
        if (isReserveTrialLessonSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Урокът бе запазен успешно",
                color: "teal",
                icon: <Check />
            });

            resetReserveTrialLessonRequestState();
            onClose();

            return;
        }

        if (isReserveTrialLessonFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на урокът",
                message: reserveTrialLessonErrorMessage,
                color: "red",
                icon: <X />
            });

            resetReserveTrialLessonRequestState();
        }
    }, [isReserveTrialLessonFailed, isReserveTrialLessonSuccessful, onClose, reserveTrialLessonErrorMessage, resetReserveTrialLessonRequestState]);

    const router = useRouter();

    return (
        <Modal size="xl" opened={isOpened} onClose={onClose} title="Запази пробен урок">
            <Group mb="sm">
                <Text>Учител:</Text>
                <Text>
                    {tutorProfile.tutorFirstName} {tutorProfile.tutorLastName}
                </Text>
            </Group>
            <Group mb="sm">
                <Text>Предмет:</Text>
                <Text>{tutorProfile.tutoringSubject}</Text>
            </Group>
            <Group mb="sm">
                <Text>Дата:</Text>
                <Text>{trialLessonDate}</Text>
            </Group>
            <Group mb="sm">
                <Text>Час:</Text>
                <Text>{trialLessonHour}</Text>
            </Group>
            <form
                onSubmit={form.onSubmit(async values => {
                    const { lessonId } = await reserveTrialLesson({
                        tutorId: tutorProfile.tutorId,
                        date: trialLessonDate,
                        startTime: trialLessonHour,
                        subject: tutorProfile.tutoringSubject,
                        grade: values.tutoringGrade
                    });

                    const response = await createCharge({ chargeAmount: tutorProfile.rateForOneHour, lessonId: lessonId, tutorId: tutorProfile.tutorId });

                    router.push(`/payments/pay/${response.paymentIntentSecret}`);
                })}
            >
                <Select
                    disabled={isReserveTrialLessonLoading}
                    {...form.getInputProps("tutoringGrade")}
                    required
                    data={tutorProfile.tutoringGrades}
                    label="Избери за кой клас ще бъде урокът"
                    mb="lg"
                    onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери клас")}
                    onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                />
                <Center>
                    <Button mt="xl" type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isReserveTrialLessonLoading}>
                        Запази час
                    </Button>
                </Center>
            </form>
        </Modal>
    );
};
