import { Box, Button, createStyles, Divider, Group, Paper, PasswordInput, Stack, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import Logo from "components/Logo";
import Link from "next/link";
import { useRouter } from "next/router";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { z } from "zod";

const loginFormSchema = z.object({
    email: z.string().email({ message: "Невалиден имейл" })
});

export const LoginForm: React.FC = () => {
    const form = useForm({
        schema: zodResolver(loginFormSchema),
        initialValues: {
            email: "",
            password: ""
        }
    });

    const theme = useMantineTheme();
    const { classes } = useStyles();
    const { login, isLoggingIn } = useAuth();
    const router = useRouter();

    return (
        <Stack align="center">
            <Group mt="xl">
                <Logo width="70px" height="70px" />
                <Title order={2} mb="xl" mt="xl">
                    Супер Учител
                </Title>
            </Group>
            <Paper className={classes.formWrapper} shadow="lg" radius="md" p="md" withBorder>
                <Group p="sm" position="apart">
                    <Title order={3}>Вход</Title>
                    <Group spacing="xs">
                        <Link href="/identity/register/tutor" passHref>
                            <Text component="a" size="xs" color={theme.primaryColor} weight="bolder">
                                Стани Супер Учител
                            </Text>
                        </Link>
                        <Text component="a" size="xs">
                            или
                        </Text>
                        <Link href="/identity/register/student" passHref>
                            <Text component="a" size="xs" color={theme.primaryColor} weight="bolder">
                                Стани Супер Ученик
                            </Text>
                        </Link>
                    </Group>
                </Group>
                <Divider m="sm" />
                <form
                    onSubmit={form.onSubmit(async values => {
                        const user = await login(values);
                        if (user) {
                            router.push("/dashboard");
                        }
                    })}
                >
                    <TextInput
                        p="sm"
                        label="Емейл"
                        required
                        placeholder="exampleuser@example.com"
                        {...form.getInputProps("email")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи имейл")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <PasswordInput
                        pl="sm"
                        pr="sm"
                        pt="sm"
                        label="Парола"
                        required
                        {...form.getInputProps("password")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля въведи парола")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    />
                    <Box p="sm" mt="xl">
                        <Button type="submit" fullWidth size="lg" loading={isLoggingIn}>
                            Вход
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
