import { Button, Center, Modal, MultiSelect, NumberInput, Select, Textarea } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import { useEffect } from "react";
import { Check, X } from "tabler-icons-react";
import { tutoringGrades } from "types/tutoringGrades";
import { tutoringSubjects } from "types/tutoringSubjects";
import { z } from "zod";
import useCreateTutorProfile from "../hooks/useCreateTutorProfile";

const CreateTutorProfileFormSchema = z.object({
    tutoringSubject: z.string().min(1, "Моля избери предмет на преподаване"),
    tutoringGrades: z.array(z.string()).min(1, "Моля избери поне един клас на преподаване"),
    about: z.string().min(1, "Моля въведи цена за час"),
    rateForOneHour: z.number().min(10, "Минималната цена за час е 10 лв.")
});

interface CreateTutorProfileModalProps {
    isOpened: boolean;
    onClose: () => void;
}

const CreateTutorProfileModal: React.FC<CreateTutorProfileModalProps> = ({ isOpened, onClose }) => {
    const form = useForm({
        schema: zodResolver(CreateTutorProfileFormSchema),
        initialValues: {
            tutoringSubject: "",
            tutoringGrades: Array<string>(),
            about: "",
            rateForOneHour: 20
        }
    });

    const { createTutorProfile, isCreateTutorProfileFailed, isCreateTutorProfileLoading, isCreateTutorProfileSuccessful, createTutorProfileErrorMessage, resetCreateTutorProfileRequestState } =
        useCreateTutorProfile();

    const tutoringSubjectSelectData = tutoringSubjects.map(tutoringSubject => ({
        value: tutoringSubject.value.toString(),
        label: tutoringSubject.name
    }));

    const tutoringGradesSelectData = tutoringGrades.map(tutoringGrade => ({
        value: tutoringGrade.value.toString(),
        label: tutoringGrade.name
    }));

    useEffect(() => {
        if (isCreateTutorProfileSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Профилът бе създаден успешно",
                color: "teal",
                icon: <Check />
            });

            resetCreateTutorProfileRequestState();
            form.reset();
            onClose();

            return;
        }

        if (isCreateTutorProfileFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: createTutorProfileErrorMessage,
                color: "red",
                icon: <X />
            });

            resetCreateTutorProfileRequestState();
        }
    }, [createTutorProfileErrorMessage, form, isCreateTutorProfileFailed, isCreateTutorProfileSuccessful, onClose, resetCreateTutorProfileRequestState]);

    return (
        <Modal size="xl" opened={isOpened} onClose={onClose} title="Създай нов профил">
            <form onSubmit={form.onSubmit(async values => createTutorProfile(values))}>
                <Select
                    disabled={isCreateTutorProfileLoading}
                    required
                    label="Предмет"
                    placeholder="Избери предмет на перподаване"
                    data={tutoringSubjectSelectData}
                    mb="lg"
                    {...form.getInputProps("tutoringSubject")}
                    onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери предмет на преподаване")}
                    onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                />
                <MultiSelect
                    disabled={isCreateTutorProfileLoading}
                    required
                    data={tutoringGradesSelectData}
                    label="Клас"
                    placeholder="Избери клас на преподаване"
                    mb="lg"
                    {...form.getInputProps("tutoringGrades")}
                    onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери поне един клас на преподаване")}
                    onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                />
                <NumberInput
                    disabled={isCreateTutorProfileLoading}
                    defaultValue={20}
                    label="Цена за час (лв)"
                    required
                    mb="lg"
                    {...form.getInputProps("rateForOneHour")}
                    onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи цена за час")}
                    onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                />
                <Textarea
                    disabled={isCreateTutorProfileLoading}
                    minRows={4}
                    placeholder="Кратко описание за опита Ви по предмета"
                    label="Описание"
                    required
                    mb="lg"
                    {...form.getInputProps("about")}
                    onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи кратко описание за опита Ви по предмета")}
                    onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                />
                <Center>
                    <Button type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isCreateTutorProfileLoading}>
                        Създай
                    </Button>
                </Center>
            </form>
        </Modal>
    );
};

export default CreateTutorProfileModal;
