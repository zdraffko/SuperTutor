import { Center, createStyles, Paper, TextInput } from "@mantine/core";
import { DatePicker } from "@mantine/dates";
import { useForm, zodResolver } from "@mantine/form";
import "dayjs/locale/bg";
import { z } from "zod";

const PersonalInformationFormSchema = z.object({
    firstName: z.string(),
    lastName: z.string(),
    dateOfBirth: z.date()
});

const PersonalInformationStep: React.FC = () => {
    const form = useForm({
        schema: zodResolver(PersonalInformationFormSchema),
        initialValues: {
            firstName: "",
            lastName: "",
            dateOfBirth: ""
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
                        label="Име"
                        required
                        placeholder="Иван"
                        {...form.getInputProps("firstName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своето име")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        p="sm"
                        label="Фамилия"
                        required
                        placeholder="Иванов"
                        {...form.getInputProps("lastName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи своята фамилия")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <DatePicker
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
