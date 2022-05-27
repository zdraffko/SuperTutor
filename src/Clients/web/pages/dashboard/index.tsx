import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { NextPage } from "next";

const DashboardPage: NextPage = () => (
    <AuthenticationProtectedPage>
        <MainLayout>Начално табло</MainLayout>
    </AuthenticationProtectedPage>
);
export default DashboardPage;
