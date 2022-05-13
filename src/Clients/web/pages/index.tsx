import { Button, Footer, Group, Stack, Text, Title, useMantineTheme } from "@mantine/core";
import type { NextPage } from "next";
import Head from "next/head";
import Link from "next/link";
import { useRouter } from "next/router";
import { useAuth } from "utils/authentication/reactQueryAuth";

const LandingPage: NextPage = () => {
    const theme = useMantineTheme();
    const { user } = useAuth();
    const router = useRouter();

    const isUserLoggedIn = !!user;
    if (isUserLoggedIn) {
        router.push("/dashboard");
    }

    return (
        <>
            <Head>
                <title>Супер Учител</title>
                <link rel="icon" href="/favicon.ico" />
            </Head>
            <Group position="right">
                <Link href="/identity/login" passHref>
                    <Button component="a" m="xl">
                        Вход
                    </Button>
                </Link>
            </Group>
            <main style={{ height: "90vh", marginTop: "10vh" }}>
                <Stack align="center" justify="center" style={{ marginTop: "10vh" }}>
                    <Title order={1}>Супер Учител</Title>
                    <Title order={4}>Започни своето приключение сега</Title>
                    <Group>
                        <Link href="/identity/register/tutor" passHref>
                            <Button component="a">Регистрация като учител</Button>
                        </Link>
                        <Link href="/identity/register/student" passHref>
                            <Button component="a">Регистрация като ученик</Button>
                        </Link>
                    </Group>
                </Stack>
            </main>
            <Footer height={60} color={theme.colors.gray[0]} p="lg">
                <Group position="center">
                    <Text>Супер Учител ©{new Date().getUTCFullYear()}</Text>
                </Group>
            </Footer>
        </>
    );
};

export default LandingPage;
