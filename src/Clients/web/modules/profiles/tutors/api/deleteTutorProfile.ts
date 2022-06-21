import { axios } from "utils/axios";

export interface DeleteTutorProfileRequest {
    tutorProfileId: string;
}

const deleteTutorProfile = async (request: DeleteTutorProfileRequest): Promise<void> => {
    const axiosResponse = await axios.post<void>("/profiles/DeleteTutorProfile", request);

    return axiosResponse.data;
};

export default deleteTutorProfile;
