import { axios } from "utils/axios";

export interface SubmitTutorProfileForReviewRequest {
    tutorProfileId: string;
}

const submitTutorProfileForReview = async (request: SubmitTutorProfileForReviewRequest): Promise<void> => {
    const axiosResponse = await axios.post<void>("/profiles/SubmitTutorProfileForReview", request);

    return axiosResponse.data;
};

export default submitTutorProfileForReview;
