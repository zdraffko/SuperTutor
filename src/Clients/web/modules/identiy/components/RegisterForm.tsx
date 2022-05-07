import { Anchor, Box, Button, Checkbox, createStyles, Divider, Group, Paper, PasswordInput, Stack, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import Link from "next/link";
import { z } from "zod";

interface RegisterFormProps {
    isTutorRegistration: boolean;
}

const registerFormSchema = z
    .object({
        email: z.string().email({ message: "Невалиден имейл" }),
        password: z.string().min(12, { message: "Паролата трябва да е с дължина поне 12 символа" }),
        confirmPassword: z.string()
    })
    .refine(data => data.confirmPassword === data.password, {
        message: "Паролите не съвпадат",
        path: ["confirmPassword"]
    });

export const RegisterForm: React.FC<RegisterFormProps> = ({ isTutorRegistration }) => {
    const form = useForm({
        schema: zodResolver(registerFormSchema),
        initialValues: {
            email: "",
            password: "",
            confirmPassword: "",
            termsOfService: false
        }
    });

    const theme = useMantineTheme();
    const { classes } = useStyles();

    return (
        <Stack align="center">
            <Title order={2} m="xl">
                Супер Учител
            </Title>
            <Paper className={classes.formWrapper} shadow="lg" radius="md" p="md" withBorder>
                <Group p="sm" position="apart">
                    <Title order={3}>Стани Супер {isTutorRegistration ? "Учител" : "Ученик"}</Title>
                    <Group spacing="xs">
                        <Text size="xs">Вече си се регистрирал?</Text>
                        <Link href="/identity/login" passHref>
                            <Text component="a" size="xs" color={theme.primaryColor} weight="bolder">
                                Влез от тук
                            </Text>
                        </Link>
                    </Group>
                </Group>
                <Divider m="sm" />
                <form onSubmit={form.onSubmit(values => console.log(values))}>
                    <TextInput
                        p="sm"
                        label="Имейл"
                        required
                        placeholder="exampleuser@example.com"
                        {...form.getInputProps("email")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи имейл")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <PasswordInput
                        description="Паролата трябва да е с дължина поне 12 символа"
                        pl="sm"
                        pr="sm"
                        label="Парола"
                        required
                        {...form.getInputProps("password")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи парола")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <PasswordInput
                        p="sm"
                        label="Потвърди паролата"
                        required
                        {...form.getInputProps("confirmPassword")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля потвърди паролата")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Group p="sm">
                        <Checkbox
                            required
                            {...form.getInputProps("termsOfService")}
                            onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Трябва да се съгласиш с условията за ползване преди да продължиш напред")}
                            onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                        />
                        <Text>
                            Съгласявам се с <Anchor>условията за ползване</Anchor>
                        </Text>
                    </Group>
                    <Box p="sm">
                        <Button type="submit" fullWidth size="lg">
                            Регистрация
                        </Button>
                    </Box>
                    <Link href="/" passHref>
                        <Text component="a" p="sm" my="xl" size="sm">
                            Начало
                        </Text>
                    </Link>
                </form>
            </Paper>
        </Stack>
    );
};

const useStyles = createStyles(() => ({
    formWrapper: {
        width: "40vw"
    }
}));
