import { User, UserType } from "utils/authentication/types";
import { axios } from "utils/axios";

interface GetIdentityInfoResponse {
    userEmail: string;
    userType: UserType;
}

export const getIdentityInfo = async (): Promise<User> => {
    const axiosResponse = await axios.get<GetIdentityInfoResponse>("/identity/GetIdentityInfo");

    return { email: axiosResponse.data.userEmail, type: axiosResponse.data.userType };
};
