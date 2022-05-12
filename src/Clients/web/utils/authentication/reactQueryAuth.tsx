import { Alert, Center, Loader } from "@mantine/core";
import { getIdentityInfo } from "modules/identiy/api/getIdentityInfo";
import { loginUser, LoginUserRequest } from "modules/identiy/api/login";
import { registerUser, RegisterUserRequest } from "modules/identiy/api/register";
import { initReactQueryAuth } from "react-query-auth";
import { AlertCircle } from "tabler-icons-react";
import authTokenStorage from "utils/authTokenStorage";
import { User } from "./types";

const load = async (): Promise<User | null> => {
    const authToken = authTokenStorage.get();
    if (authToken === null) {
        return null;
    }

    return await getIdentityInfo();
};

const login = async (loginUserRequest: LoginUserRequest): Promise<User | null> => {
    const loginUserResponse = await loginUser(loginUserRequest);
    const { authToken } = loginUserResponse;

    authTokenStorage.set(authToken);

    return await getIdentityInfo();
};

const register = async (registerUserRequest: RegisterUserRequest): Promise<User | null> => {
    const registerUserResponse = await registerUser(registerUserRequest);
    const { authToken } = registerUserResponse;

    authTokenStorage.set(authToken);

    return await getIdentityInfo();
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

export const { AuthProvider, useAuth } = initReactQueryAuth<User | null, unknown, LoginUserRequest, RegisterUserRequest>(reactQueryAuthConfig);
