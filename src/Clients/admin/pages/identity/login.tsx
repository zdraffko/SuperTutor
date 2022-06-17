import { LoginForm } from "modules/identiy";
import type { NextPage } from "next";
import Head from "next/head";

const LoginPage: NextPage = () => (
    <>
        <Head>
            <title>Вход</title>
            <link rel="icon" href="/favicon.ico" />
        </Head>
        <LoginForm />
    </>
);

export default LoginPage;
