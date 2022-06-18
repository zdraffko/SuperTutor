import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { TutorProfilesForReview } from "modules/profiles";
import { NextPage } from "next";

const DashboardPage: NextPage = () => (
    <AuthenticationProtectedPage>
        <MainLayout>
            <TutorProfilesForReview />
        </MainLayout>
    </AuthenticationProtectedPage>
);
export default DashboardPage;
