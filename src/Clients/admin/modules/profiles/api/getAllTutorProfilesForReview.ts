import { axios } from "utils/axios";
import { TutorProfile } from "../types";

export interface GetAllTutorProfilesForReviewResponse {
    tutorProfiles: TutorProfile[];
}

const getAllTutorProfilesForReview = async (): Promise<GetAllTutorProfilesForReviewResponse> => {
    const axiosResponse = await axios.get<GetAllTutorProfilesForReviewResponse>("/profiles/getAllTutorProfilesForReview");

    return axiosResponse.data;
};

export default getAllTutorProfilesForReview;
