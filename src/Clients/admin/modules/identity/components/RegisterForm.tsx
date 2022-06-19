import { Box, Button, createStyles, Divider, Group, Paper, PasswordInput, Stack, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import Link from "next/link";
import { useRouter } from "next/router";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { z } from "zod";

interface RegisterFormProps {
    isTutorRegistration: boolean;
}

const registerFormSchema = z
    .object({
        firstName: z.string().min(1, "Моля въведи име"),
        lastName: z.string().min(1, "Моля въведи фамилия"),
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
            firstName: "",
            lastName: "",
            email: "",
            password: "",
            confirmPassword: "",
            termsOfService: false
        }
    });

    const theme = useMantineTheme();
    const { classes } = useStyles();
    const { register, isRegistering } = useAuth();
    const router = useRouter();

    return (
        <Stack align="center" mb="xl">
            <Group mt="xl">
                <Title order={2} mt="xl" mb="xl">
                    Супер Учител
                </Title>
            </Group>
            <Paper className={classes.formWrapper} shadow="lg" radius="md" p="md" withBorder>
                <Group p="sm" position="apart">
                    <Title order={3}>Стани Супер Администратор</Title>
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
                <form
                    onSubmit={form.onSubmit(async values => {
                        const user = await register({ ...values });
                        if (user) {
                            router.push("/dashboard");
                        }
                    })}
                >
                    <TextInput
                        p="sm"
                        label="Име"
                        required
                        placeholder="Иван"
                        {...form.getInputProps("firstName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи име")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        pl="sm"
                        pr="sm"
                        pb="sm"
                        label="Фамилия"
                        required
                        placeholder="Иванов"
                        {...form.getInputProps("lastName")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи фамилия")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <TextInput
                        pl="sm"
                        pr="sm"
                        pb="sm"
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
                        pb="sm"
                        label="Парола"
                        required
                        {...form.getInputProps("password")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи парола")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <PasswordInput
                        pl="sm"
                        pr="sm"
                        pb="sm"
                        label="Потвърди паролата"
                        required
                        {...form.getInputProps("confirmPassword")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля потвърди паролата")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box p="sm">
                        <Button type="submit" fullWidth size="lg" loading={isRegistering}>
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
