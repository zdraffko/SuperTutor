import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";

const DashboardPage: React.FC = () => (
    <AuthenticationProtectedPage>
        <MainLayout>Начално табло</MainLayout>
    </AuthenticationProtectedPage>
);
export default DashboardPage;
