import { axios } from "utils/axios";

export type LoginUserRequest = {
    email: string;
    password: string;
};

export interface LoginUserResponse {
    authToken: string;
}

export const loginUser = async (request: LoginUserRequest): Promise<LoginUserResponse> => await axios.post("/identity/login", request);
