import { axios } from "utils/axios";

export interface GetIsTutorPersonalInformationCollectedResponse {
    isPersonalInformationCollected: boolean;
}

const getIsTutorPersonalInformationCollected = async (): Promise<GetIsTutorPersonalInformationCollectedResponse> => {
    const axiosResponse = await axios.get<GetIsTutorPersonalInformationCollectedResponse>("/payments/GetIsTutorPersonalInformationCollected");

    return axiosResponse.data;
};

export default getIsTutorPersonalInformationCollected;
