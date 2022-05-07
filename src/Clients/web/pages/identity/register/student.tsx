import { RegisterForm } from "modules/identiy";
import type { NextPage } from "next";

const StudentRegistrationPage: NextPage = () => <RegisterForm isTutorRegistration={false} />;

export default StudentRegistrationPage;
