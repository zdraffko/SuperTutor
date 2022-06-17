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
                <title>Админ - Супер Учител</title>
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
                    <Group>
                        <Title order={1}>Супер Учител</Title>
                    </Group>
                    <Group>
                        <Link href="/identity/register" passHref>
                            <Button component="a">Създай нов администраторски профил</Button>
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
