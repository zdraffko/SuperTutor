import { Anchor, Box, Button, Checkbox, createStyles, Divider, Group, Paper, PasswordInput, Stack, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import Logo from "components/Logo";
import Link from "next/link";
import { useRouter } from "next/router";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";
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
    const { register, isRegistering } = useAuth();
    const router = useRouter();

    return (
        <Stack align="center">
            <Group mt="xl">
                <Logo width="70px" height="70px" />
                <Title order={2} mt="xl" mb="xl">
                    Супер Учител
                </Title>
            </Group>
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
                <form
                    onSubmit={form.onSubmit(async values => {
                        const userType = isTutorRegistration ? UserType.Tutor : UserType.Student;
                        const registerRequest = { ...values, type: userType };

                        await register(registerRequest);

                        router.push("/dashboard");
                    })}
                >
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
