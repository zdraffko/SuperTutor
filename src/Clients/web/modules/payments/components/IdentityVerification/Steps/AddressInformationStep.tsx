import { Center, createStyles, NumberInput, Paper, TextInput } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import "dayjs/locale/bg";
import { z } from "zod";

const AddressInformationFormSchema = z.object({
    state: z.string(),
    city: z.string(),
    addressLineOne: z.string(),
    addressLineTwo: z.string(),
    postalCode: z.number()
});

const AddressInformationStep: React.FC = () => {
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
                        label="Община"
                        required
                        placeholder="София"
                        {...form.getInputProps("state")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята община")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        p="sm"
                        label="Град"
                        required
                        placeholder="София"
                        {...form.getInputProps("city")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своят град")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        p="sm"
                        label="Улица"
                        required
                        placeholder='ул. "Филип Тотю"'
                        {...form.getInputProps("addressLineOne")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята улица")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        p="sm"
                        label="Сграда"
                        required
                        placeholder='номер 15, вход "А", апартамент 4'
                        {...form.getInputProps("addressLineTwo")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята улица")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <NumberInput
                        p="sm"
                        mb="xl"
                        label="Пощенски код"
                        required
                        hideControls
                        placeholder="1000"
                        {...form.getInputProps("postalCode")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своят адрес")}
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

export default AddressInformationStep;
