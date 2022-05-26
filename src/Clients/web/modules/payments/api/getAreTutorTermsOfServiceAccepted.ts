import { axios } from "utils/axios";

export interface GetAreTutorTermsOfServiceAcceptedResponse {
    areTermsOfServiceAccepted: boolean;
}

const getAreTutorTermsOfServiceAccepted = async (): Promise<GetAreTutorTermsOfServiceAcceptedResponse> => {
    const axiosResponse = await axios.get<GetAreTutorTermsOfServiceAcceptedResponse>("/payments/GetAreTutorTermsOfServiceAccepted");

    return axiosResponse.data;
};

export default getAreTutorTermsOfServiceAccepted;
