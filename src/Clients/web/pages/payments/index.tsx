import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";

const PaymentsPage: React.FC = () => (
    <AuthenticationProtectedPage>
        <MainLayout>Плащания</MainLayout>
    </AuthenticationProtectedPage>
);
export default PaymentsPage;
