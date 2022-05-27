import { axios } from "utils/axios";

export interface CreateTutorProfileRequest {
    tutoringSubject: string;
    tutoringGrades: string[];
    about: string;
    rateForOneHour: number;
}

export interface CreateTutorProfileResponse {
    tutorProfileId: string;
}

const createTutorProfile = async (request: CreateTutorProfileRequest): Promise<CreateTutorProfileResponse> => {
    const axiosResponse = await axios.post<CreateTutorProfileResponse>("/profiles/CreateTutorProfile", request);

    return axiosResponse.data;
};

export default createTutorProfile;
