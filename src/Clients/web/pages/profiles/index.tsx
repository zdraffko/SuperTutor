import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { StudentProfileDashboard, TutorProfilesDashboard } from "modules/profiles";
import { NextPage } from "next";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

const ProfilesPage: NextPage = () => {
    const { user } = useAuth();

    return (
        <AuthenticationProtectedPage>
            <MainLayout>{user?.type === UserType.Tutor ? <TutorProfilesDashboard /> : <StudentProfileDashboard />}</MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default ProfilesPage;
