import { axios } from "utils/axios";
import { TutorProfile } from "../types/tutorProfile";

export interface GetAllTutorProfilesForTutorResponse {
    tutorProfiles: TutorProfile[];
}

const getAllTutorProfilesForTutor = async (): Promise<GetAllTutorProfilesForTutorResponse> => {
    const axiosResponse = await axios.get<GetAllTutorProfilesForTutorResponse>("/profiles/GetAllTutorProfilesForTutor");

    return axiosResponse.data;
};

export default getAllTutorProfilesForTutor;
