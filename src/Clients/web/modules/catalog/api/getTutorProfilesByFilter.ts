import { axios } from "utils/axios";
import { CatalogTutorProfile } from "../types";

export interface GetTutorProfilesByFilterRequest {
    tutoringSubject: string | null;
    tutoringGrades: string[] | null;
    minRateForOneHour: number | null;
    maxRateForOneHour: number | null;
}

export interface GetTutorProfilesByFilterResponse {
    tutorProfiles: CatalogTutorProfile[];
}

const getTutorProfilesByFilter = async (request: GetTutorProfilesByFilterRequest): Promise<GetTutorProfilesByFilterResponse> => {
    const query = JSON.stringify(request);
    const axiosResponse = await axios.get<GetTutorProfilesByFilterResponse>(`/catalog/GetTutorProfilesByFilter?query=${query}`);

    return axiosResponse.data;
};

export default getTutorProfilesByFilter;
