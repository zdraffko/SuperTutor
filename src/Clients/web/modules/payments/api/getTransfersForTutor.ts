import { axios } from "utils/axios";
import { Transfer } from "../types";

export interface GetTransfersForTutorResponse {
    transfers: Transfer[];
}

const getTransfersForTutor = async (): Promise<GetTransfersForTutorResponse> => {
    const axiosResponse = await axios.get<GetTransfersForTutorResponse>("/payments/GetTransfersForTutor");

    return axiosResponse.data;
};

export default getTransfersForTutor;
