import { Anchor, Box, Button, Checkbox, createStyles, Divider, Group, Paper, PasswordInput, Text, TextInput, Title, useMantineTheme } from "@mantine/core";
import { useForm } from "@mantine/form";
import Link from "next/link";

interface RegisterFormProps {
    isTutorRegistration: boolean;
}

export const RegisterForm: React.FC<RegisterFormProps> = ({ isTutorRegistration }) => {
    const form = useForm({
        initialValues: {
            name: "",
            userName: "",
            email: "",
            password: "",
            confirmPassword: "",
            termsOfService: false
        },

        validate: {
            email: value => (/^\S+@\S+$/.test(value) ? null : "Invalid email")
        }
    });

    const theme = useMantineTheme();
    const { classes } = useStyles();

    return (
        <Group direction="column" align="center">
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
                        label="Емейл"
                        required
                        placeholder="exampleuser@example.com"
                        value={form.values.email}
                        error={form.errors.email && "Please specify valid email"}
                        onChange={event => form.setFieldValue("email", event.currentTarget.value)}
                    />
                    <PasswordInput pl="sm" pr="sm" pt="sm" label="Парола" required value={form.values.password} onChange={event => form.setFieldValue("password", event.currentTarget.value)} />
                    <Text align="left" size="xs" mx="sm" color="">
                        Паролата трябва да е с дължина поне 12 симвула
                    </Text>
                    <PasswordInput p="sm" label="Потвърди паролата" required value={form.values.confirmPassword} onChange={event => form.setFieldValue("confirmPassword", event.currentTarget.value)} />
                    <Group p="sm">
                        <Checkbox required checked={form.values.termsOfService} onChange={() => form.setFieldValue("termsOfService", !form.values.termsOfService)} />
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
                            Обратно
                        </Text>
                    </Link>
                </form>
            </Paper>
        </Group>
    );
};

const useStyles = createStyles(() => ({
    formWrapper: {
        width: "40vw"
    }
}));
