import { User } from "utils/authentication/types";
import { axios } from "utils/axios";

interface GetIdentityInfoResponse {
    userId: string;
    userEmail: string;
    firstName: string;
    lastName: string;
}

export const getIdentityInfo = async (): Promise<User> => {
    const axiosResponse = await axios.get<GetIdentityInfoResponse>("/identity/GetIdentityInfo");

    return {
        id: axiosResponse.data.userId,
        email: axiosResponse.data.userEmail,
        firstName: axiosResponse.data.firstName,
        lastName: axiosResponse.data.lastName
    };
};
