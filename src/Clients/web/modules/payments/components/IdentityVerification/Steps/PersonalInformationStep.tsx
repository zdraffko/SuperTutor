import { Box, Button, Center, createStyles, Paper, TextInput } from "@mantine/core";
import { DatePicker } from "@mantine/dates";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import "dayjs/locale/bg";
import useUpdatePersonalInformation from "modules/payments/hooks/useUpdatePersonalInformation";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { convertDateToDateOnlyString } from "utils/dates";
import { z } from "zod";

const PersonalInformationFormSchema = z.object({
    firstName: z.string(),
    lastName: z.string(),
    dateOfBirth: z.date()
});

interface PersonalInformationStepProps {
    goToNextStep: () => void;
}

const PersonalInformationStep: React.FC<PersonalInformationStepProps> = ({ goToNextStep }) => {
    const { classes } = useStyles();
    const {
        updatePersonalInformation,
        isUpdatePersonalInformationSuccessful,
        isUpdatePersonalInformationFailed,
        isUpdatePersonalInformationLoading,
        updatePersonalInformationErrorMessage,
        resetUpdatePersonalInformationRequestState
    } = useUpdatePersonalInformation();
    const form = useForm({
        schema: zodResolver(PersonalInformationFormSchema),
        initialValues: {
            firstName: "",
            lastName: "",
            dateOfBirth: " "
        }
    });

    useEffect(() => {
        if (isUpdatePersonalInformationSuccessful) {
            goToNextStep();
        }

        if (isUpdatePersonalInformationFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: updatePersonalInformationErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        resetUpdatePersonalInformationRequestState();
    }, [goToNextStep, isUpdatePersonalInformationFailed, isUpdatePersonalInformationSuccessful, resetUpdatePersonalInformationRequestState, updatePersonalInformationErrorMessage]);

    return (
        <Center m="xl">
            <Paper className={classes.formWrapper} shadow="sm" radius="md" p="md" withBorder>
                <form
                    onSubmit={form.onSubmit(
                        async values =>
                            await updatePersonalInformation({
                                firstName: values.firstName,
                                lastName: values.lastName,
                                dateOfBirth: convertDateToDateOnlyString(new Date(values.dateOfBirth))
                            })
                    )}
                >
                    <TextInput
                        disabled={isUpdatePersonalInformationLoading}
                        p="sm"
                        label="Име"
                        required
                        placeholder="Иван"
                        {...form.getInputProps("firstName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своето име")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        disabled={isUpdatePersonalInformationLoading}
                        p="sm"
                        label="Фамилия"
                        required
                        placeholder="Иванов"
                        {...form.getInputProps("lastName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята фамилия")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <DatePicker
                        disabled={isUpdatePersonalInformationLoading}
                        p="sm"
                        mb="xl"
                        label="Дата на раждане"
                        required
                        placeholder="Март 14, 1989"
                        locale="bg"
                        dropdownType="modal"
                        {...form.getInputProps("dateOfBirth")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята дата на раждане")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                        //minDate={dayjs(new Date()).startOf("month").add(5, "days").toDate()}
                        //maxDate={dayjs(new Date()).endOf("month").subtract(5, "days").toDate()}
                    />
                    <Box p="sm" mt="xl">
                        <Center>
                            <Button type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isUpdatePersonalInformationLoading}>
                                Запиши и продължи напред
                            </Button>
                        </Center>
                    </Box>
                </form>
            </Paper>
        </Center>
    );
};

const useStyles = createStyles(() => ({
    formWrapper: {
        width: "45vw"
    }
}));

export default PersonalInformationStep;
