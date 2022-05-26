import { axios } from "utils/axios";

export interface GetIsTutorAddressInformationCollectedResponse {
    isAddressInformationCollected: boolean;
}

const getIsTutorAddressInformationCollected = async (): Promise<GetIsTutorAddressInformationCollectedResponse> => {
    const axiosResponse = await axios.get<GetIsTutorAddressInformationCollectedResponse>("/payments/GetIsTutorAddressInformationCollected");

    return axiosResponse.data;
};

export default getIsTutorAddressInformationCollected;
