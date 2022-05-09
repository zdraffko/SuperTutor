import { UserType } from "utils/authentication/types";
import { axios } from "utils/axios";

export interface RegisterUserRequest {
    email: string;
    password: string;
    type: UserType;
}

export interface RegisterUserResponse {
    authToken: string;
}

export const registerUser = async (request: RegisterUserRequest): Promise<RegisterUserResponse> => {
    const registerUserRequestWithoutUserType = {
        email: request.email,
        password: request.password
    };

    if (request.type === UserType.Tutor) {
        return await axios.post("/identity/registerTutor", registerUserRequestWithoutUserType);
    }

    return await axios.post("/identity/registerStudent", registerUserRequestWithoutUserType);
};
