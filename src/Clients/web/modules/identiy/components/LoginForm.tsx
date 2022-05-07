import { Box, Button, createStyles, Divider, Group, Paper, PasswordInput, Stack, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import Link from "next/link";
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

    return (
        <Stack align="center">
            <Title order={2} m="xl">
                Супер Учител
            </Title>
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
                <form onSubmit={form.onSubmit(values => console.log(values))}>
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
                        <Button type="submit" fullWidth size="lg">
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
