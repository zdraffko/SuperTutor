import { Alert, Center, Loader } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { getIdentityInfo } from "modules/identity/api/getIdentityInfo";
import { loginUser, LoginUserRequest } from "modules/identity/api/login";
import { registerUser, RegisterUserRequest } from "modules/identity/api/register";
import { initReactQueryAuth } from "react-query-auth";
import { AlertCircle, X } from "tabler-icons-react";
import authTokenStorage from "utils/authTokenStorage";
import { User } from "./types";

interface AuthError {
    message: string;
}

const load = async (): Promise<User | null> => {
    const authToken = authTokenStorage.get();
    if (authToken === null) {
        return null;
    }

    return await getIdentityInfo();
};

const login = async (loginUserRequest: LoginUserRequest): Promise<User | null> => {
    try {
        const loginUserResponse = await loginUser(loginUserRequest);
        const { authToken } = loginUserResponse;

        authTokenStorage.set(authToken);

        return await getIdentityInfo();
    } catch (error) {
        showNotification({
            autoClose: 5000,
            title: "Възникна проблем при влизането",
            message: error.response?.data,
            color: "red",
            icon: <X />
        });

        return null;
    }
};

const register = async (registerUserRequest: RegisterUserRequest): Promise<User | null> => {
    try {
        const registerUserResponse = await registerUser(registerUserRequest);
        const { authToken } = registerUserResponse;

        authTokenStorage.set(authToken);

        return await getIdentityInfo();
    } catch (error) {
        showNotification({
            autoClose: 5000,
            title: "Възникна проблем при регистрацията",
            message: error.response?.data,
            color: "red",
            icon: <X />
        });

        return null;
    }
};

const logout = async (): Promise<any> => {
    authTokenStorage.clear();
    window.location.assign(window.location.origin);
};

const reactQueryAuthConfig = {
    key: "identity-user",
    loadUser: load,
    loginFn: login,
    registerFn: register,
    logoutFn: logout,
    LoaderComponent: () => (
        <Center style={{ width: "100vw", height: "100vh" }}>
            <Loader size="lg" />
        </Center>
    ),
    ErrorComponent: () => (
        <Center style={{ width: "100vw", height: "100vh" }}>
            <Alert icon={<AlertCircle size={16} />} title="Опа!" color="red">
                Възникна неочаквана грешка, моля опитайте пак.
            </Alert>
        </Center>
    )
};

export const { AuthProvider, useAuth } = initReactQueryAuth<User | null, AuthError, LoginUserRequest, RegisterUserRequest>(reactQueryAuthConfig);
