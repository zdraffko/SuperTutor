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
    const axiosResponse = await axios.post("/identity/register", request);

    return axiosResponse.data;
};
