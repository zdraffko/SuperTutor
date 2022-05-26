import { axios } from "utils/axios";

export interface GetIsTutorBankAccountInformationCollectedResponse {
    isBankAccountInformationCollected: boolean;
}

const getIsTutorBankAccountInformationCollected = async (): Promise<GetIsTutorBankAccountInformationCollectedResponse> => {
    const axiosResponse = await axios.get<GetIsTutorBankAccountInformationCollectedResponse>("/payments/GetIsTutorBankAccountInformationCollected");

    return axiosResponse.data;
};

export default getIsTutorBankAccountInformationCollected;
