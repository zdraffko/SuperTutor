import { axios } from "utils/axios";

export interface GetAreTutorVerificationDocumentsCollectedResponse {
    areVerificationDocumentsCollected: boolean;
}

const getAreTutorVerificationDocumentsCollected = async (): Promise<GetAreTutorVerificationDocumentsCollectedResponse> => {
    const axiosResponse = await axios.get<GetAreTutorVerificationDocumentsCollectedResponse>("/payments/GetAreTutorVerificationDocumentsCollected");

    return axiosResponse.data;
};

export default getAreTutorVerificationDocumentsCollected;
