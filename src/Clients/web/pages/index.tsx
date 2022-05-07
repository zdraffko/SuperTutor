import { Button, Footer, Group, Text } from "@mantine/core";
import type { NextPage } from "next";
import Head from "next/head";
import Link from "next/link";

const HomePage: NextPage = () => (
    <>
        <Head>
            <title>Супер Учител</title>
            <link rel="icon" href="/favicon.ico" />
        </Head>

        <main>
            <h1>Супер Учител</h1>
            <Group>
                <Link href="/identity/login" passHref>
                    <Button component="a">Вход</Button>
                </Link>
                <Link href="/identity/register/tutor" passHref>
                    <Button component="a">Регистрация като учител</Button>
                </Link>
                <Link href="/identity/register/student" passHref>
                    <Button component="a">Регистрация като ученик</Button>
                </Link>
            </Group>
        </main>

        <Footer height={60}>
            <Text>Супер Учител @{new Date().getUTCFullYear()}</Text>
        </Footer>
    </>
);

export default HomePage;
