import { RegisterForm } from "modules/identity";
import type { NextPage } from "next";
import Head from "next/head";

const TutorRegistrationPage: NextPage = () => (
    <>
        <Head>
            <title>Регистрация като учител</title>
            <link rel="icon" href="/favicon.ico" />
        </Head>
        <RegisterForm isTutorRegistration />
    </>
);

export default TutorRegistrationPage;
