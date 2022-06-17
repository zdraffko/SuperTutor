import { useRouter } from "next/router";
import { ReactNode } from "react";
import { useAuth } from "utils/authentication/reactQueryAuth";

interface AuthenticationProtectedPageProps {
    children: ReactNode;
}

const AuthenticationProtectedPage: React.FC<AuthenticationProtectedPageProps> = ({ children }) => {
    const { user } = useAuth();
    const router = useRouter();

    const isUserLoggedIn = !!user;
    if (!isUserLoggedIn) {
        router.push("/identity/login");
    }

    return <>{children}</>;
};
export default AuthenticationProtectedPage;
