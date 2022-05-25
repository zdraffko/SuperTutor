import { Box, Button, Center, createStyles, NumberInput, Paper, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import "dayjs/locale/bg";
import useUpdateAddressInformation from "modules/payments/hooks/useUpdateAddressInformation";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { z } from "zod";

const AddressInformationFormSchema = z.object({
    state: z.string(),
    city: z.string(),
    addressLineOne: z.string(),
    addressLineTwo: z.string(),
    postalCode: z.number()
});

interface AddressInformationStepProps {
    goToNextStep: () => void;
}

const AddressInformationStep: React.FC<AddressInformationStepProps> = ({ goToNextStep }) => {
    const { user } = useAuth();
    const { classes } = useStyles();
    const {
        updateAddressInformation,
        isUpdateAddressInformationFailed,
        isUpdateAddressInformationLoading,
        isUpdateAddressInformationSuccessful,
        updateAddressInformationErrorMessage,
        resetUpdateAddressInformationRequestState
    } = useUpdateAddressInformation();

    const form = useForm({
        schema: zodResolver(AddressInformationFormSchema),
        initialValues: {
            state: "",
            city: "",
            addressLineOne: "",
            addressLineTwo: "",
            postalCode: ""
        }
    });

    useEffect(() => {
        if (isUpdateAddressInformationSuccessful) {
            goToNextStep();
        }

        if (isUpdateAddressInformationFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: updateAddressInformationErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        resetUpdateAddressInformationRequestState();
    }, [goToNextStep, isUpdateAddressInformationFailed, isUpdateAddressInformationSuccessful, resetUpdateAddressInformationRequestState, updateAddressInformationErrorMessage]);

    return (
        <Center m="xl">
            <Paper className={classes.formWrapper} shadow="sm" radius="md" p="md" withBorder>
                <form onSubmit={form.onSubmit(async values => updateAddressInformation({ tutorId: user!.id, ...values }))}>
                    <TextInput
                        disabled={isUpdateAddressInformationLoading}
                        p="sm"
                        label="Община"
                        required
                        placeholder="София"
                        {...form.getInputProps("state")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята община")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        disabled={isUpdateAddressInformationLoading}
                        p="sm"
                        label="Град"
                        required
                        placeholder="София"
                        {...form.getInputProps("city")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своят град")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        disabled={isUpdateAddressInformationLoading}
                        p="sm"
                        label="Улица"
                        required
                        placeholder='ул. "Филип Тотю"'
                        {...form.getInputProps("addressLineOne")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята улица")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        disabled={isUpdateAddressInformationLoading}
                        p="sm"
                        label="Сграда"
                        required
                        placeholder='номер 15, вход "А", апартамент 4'
                        {...form.getInputProps("addressLineTwo")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи информация за своята сграда")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <NumberInput
                        disabled={isUpdateAddressInformationLoading}
                        p="sm"
                        mb="xl"
                        label="Пощенски код"
                        required
                        hideControls
                        placeholder="1000"
                        {...form.getInputProps("postalCode")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своят пощенски код")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box p="sm" mt="xl">
                        <Center>
                            <Button type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isUpdateAddressInformationLoading}>
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

export default AddressInformationStep;
