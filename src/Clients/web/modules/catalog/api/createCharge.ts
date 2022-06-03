import { axios } from "utils/axios";

export interface CreateChargeRequest {
    chargeAmount: number;
    lessonId: string;
    tutorId: string;
}

export interface CreateChargeResponse {
    paymentIntentSecret: string;
}

const createCharge = async (request: CreateChargeRequest): Promise<CreateChargeResponse> => {
    const axiosResponse = await axios.post<CreateChargeResponse>("/payments/CreateCharge", request);

    return axiosResponse.data;
};

export default createCharge;
