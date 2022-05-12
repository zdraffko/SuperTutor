import { User } from "utils/authentication/types";
import { axios } from "utils/axios";

export const getIdentityInfo = async (): Promise<User> => {
    const axiosResponse = await axios.get<User>("/identity/GetIdentityInfo");

    return axiosResponse.data;
};
