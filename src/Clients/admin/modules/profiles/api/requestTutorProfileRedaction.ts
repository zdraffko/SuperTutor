import { axios } from "utils/axios";

export interface RequestTutorProfileRedactionRequest {
    tutorProfileId: string;
    comment: string;
}

const requestTutorProfileRedaction = async (request: RequestTutorProfileRedactionRequest): Promise<void> => {
    const axiosResponse = await axios.post("/profiles/RequestTutorProfileRedaction", request);

    return axiosResponse.data;
};

export default requestTutorProfileRedaction;
