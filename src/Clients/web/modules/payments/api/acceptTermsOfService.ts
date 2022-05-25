import { axios } from "utils/axios";

export interface AcceptTermsOfServiceRequest {
    userId: string;
}

const acceptTermsOfService = async (request: AcceptTermsOfServiceRequest): Promise<void> => await axios.post("/payments/AcceptTermsOfService", request);

export default acceptTermsOfService;
