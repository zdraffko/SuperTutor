import { axios } from "utils/axios";

export interface UpdateTutorProfileAboutRequest {
    tutorProfileId: string;
    newAbout: string;
}

const updateTutorProfileAbout = async (request: UpdateTutorProfileAboutRequest): Promise<void> => {
    const axiosResponse = await axios.post<void>("/profiles/UpdateTutorProfileAbout", request);

    return axiosResponse.data;
};

export default updateTutorProfileAbout;
