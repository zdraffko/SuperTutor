import { Box, Button, Center, createStyles, Paper, Select, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import useUpdatePayoutInformation from "modules/payments/hooks/useUpdatePayoutInformation";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { z } from "zod";

const PayoutInformationFormSchema = z.object({
    bankAccountHolderFullName: z.string(),
    bankAccountHolderType: z.string(),
    bankAccountIban: z.string()
});

interface PayoutInformationStepProps {
    goToNextStep: () => void;
}

const PayoutInformationStep: React.FC<PayoutInformationStepProps> = ({ goToNextStep }) => {
    const { classes } = useStyles();
    const {
        updatePayoutInformation,
        isUpdatePayoutInformationFailed,
        isUpdatePayoutInformationLoading,
        isUpdatePayoutInformationSuccessful,
        updatePayoutInformationErrorMessage,
        resetUpdatePayoutInformationRequestState
    } = useUpdatePayoutInformation();
    const form = useForm({
        schema: zodResolver(PayoutInformationFormSchema),
        initialValues: {
            bankAccountHolderFullName: "",
            bankAccountHolderType: "individual",
            bankAccountIban: ""
        }
    });

    useEffect(() => {
        if (isUpdatePayoutInformationSuccessful) {
            goToNextStep();
        }

        if (isUpdatePayoutInformationFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: updatePayoutInformationErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        resetUpdatePayoutInformationRequestState();
    }, [goToNextStep, isUpdatePayoutInformationFailed, isUpdatePayoutInformationSuccessful, resetUpdatePayoutInformationRequestState, updatePayoutInformationErrorMessage]);

    return (
        <Center m="xl">
            <Paper className={classes.formWrapper} shadow="sm" radius="md" p="md" withBorder>
                <form onSubmit={form.onSubmit(async values => await updatePayoutInformation(values))}>
                    <TextInput
                        disabled={isUpdatePayoutInformationLoading}
                        p="sm"
                        label="Притежател на сметката"
                        required
                        placeholder="ИВАН ИВАНОВ"
                        {...form.getInputProps("bankAccountHolderFullName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи имената на притежателя на сметката")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Select
                        disabled={isUpdatePayoutInformationLoading}
                        p="sm"
                        label="Вид на притежателят"
                        required
                        //defaultValue="individual"
                        data={[
                            { value: "individual", label: "Индивидуален клиент" },
                            { value: "company", label: "Корпоративен клиент" }
                        ]}
                        {...form.getInputProps("bankAccountHolderType")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи вид на притежателят")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        disabled={isUpdatePayoutInformationLoading}
                        p="sm"
                        mb="xl"
                        label="IBAN на сметката"
                        required
                        placeholder="BG80BNBG96611020345678"
                        {...form.getInputProps("bankAccountIban")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи IBAN на сметката")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box p="sm" mt="xl">
                        <Center>
                            <Button type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isUpdatePayoutInformationLoading}>
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

export default PayoutInformationStep;
