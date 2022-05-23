import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { IdentityVerification } from "modules/payments";

const PaymentsPage: React.FC = () => (
    <AuthenticationProtectedPage>
        <MainLayout>
            <IdentityVerification />
        </MainLayout>
    </AuthenticationProtectedPage>
);
export default PaymentsPage;
