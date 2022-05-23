import { Center, createStyles, Paper, Select, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import "dayjs/locale/bg";
import { z } from "zod";

const PayoutInformationFormSchema = z.object({
    bankAccountHolderFullName: z.string(),
    bankAccountHolderType: z.string(),
    bankAccountIban: z.string()
});

const PayoutInformationStep: React.FC = () => {
    const form = useForm({
        schema: zodResolver(PayoutInformationFormSchema),
        initialValues: {
            bankAccountHolderFullName: "",
            bankAccountHolderType: "individual",
            bankAccountIban: ""
        }
    });

    const { classes } = useStyles();

    return (
        <Center m="xl">
            <Paper className={classes.formWrapper} shadow="sm" radius="md" p="md" withBorder>
                <form
                    onSubmit={form.onSubmit(async values => {
                        console.log("first step completed");
                    })}
                >
                    <TextInput
                        p="sm"
                        label="Притежател на сметката"
                        required
                        placeholder="ИВАН ИВАНОВ"
                        {...form.getInputProps("bankAccountHolderFullName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи имената на притежателя на сметката")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Select
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
                        p="sm"
                        mb="xl"
                        label="IBAN на сметката"
                        required
                        placeholder="BG80BNBG96611020345678"
                        {...form.getInputProps("bankAccountIban")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи IBAN на сметката")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
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
