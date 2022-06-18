import { axios } from "utils/axios";

export interface ApproveTutorProfileRequest {
    tutorProfileId: string;
}

const approveTutorProfile = async (request: ApproveTutorProfileRequest): Promise<void> => {
    const axiosResponse = await axios.post("/profiles/ApproveTutorProfile", request);

    return axiosResponse.data;
};

export default approveTutorProfile;
