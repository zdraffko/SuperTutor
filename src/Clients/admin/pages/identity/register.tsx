import { RegisterForm } from "modules/identiy";
import type { NextPage } from "next";
import Head from "next/head";

const TutorRegistrationPage: NextPage = () => (
    <>
        <Head>
            <title>Регистрация</title>
            <link rel="icon" href="/favicon.ico" />
        </Head>
        <RegisterForm isTutorRegistration />
    </>
);

export default TutorRegistrationPage;
