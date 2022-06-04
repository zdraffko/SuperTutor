import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { StudentCalendar, TutorCalendar } from "modules/calendar";
import { NextPage } from "next";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

const CalendarPage: NextPage = () => {
    const { user } = useAuth();

    return (
        <AuthenticationProtectedPage>
            <MainLayout>{user?.type == UserType.Tutor ? <TutorCalendar /> : <StudentCalendar />}</MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default CalendarPage;
