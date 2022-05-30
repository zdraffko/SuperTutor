import { axios } from "utils/axios";
import { TutorAvailability } from "../types";

export interface GetTutorAvailabilityRequest {
    tutorId: string;
}

export interface GetTutorAvailabilityResponse {
    availabilities: TutorAvailability[];
}

const getTutorAvailability = async (request: GetTutorAvailabilityRequest): Promise<GetTutorAvailabilityResponse> => {
    const query = JSON.stringify(request);
    const axiosResponse = await axios.get<GetTutorAvailabilityResponse>(`/catalog/GetTutorAvailability?query=${query}`);

    return axiosResponse.data;
};

export default getTutorAvailability;
