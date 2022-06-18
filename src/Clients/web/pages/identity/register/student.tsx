import { RegisterForm } from "modules/identity";
import type { NextPage } from "next";
import Head from "next/head";

const StudentRegistrationPage: NextPage = () => (
    <>
        <Head>
            <title>Регистрация като ученик</title>
            <link rel="icon" href="/favicon.ico" />
        </Head>
        <RegisterForm isTutorRegistration={false} />
    </>
);

export default StudentRegistrationPage;
