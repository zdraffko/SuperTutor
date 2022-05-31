import { axios } from "utils/axios";
import { CatalogTutorProfile } from "../types";

export interface GetTutorProfileByIdRequest {
    tutorProfileId: string;
}

export interface GetTutorProfileByIdResponse {
    profile: CatalogTutorProfile;
}

const getTutorProfileById = async (request: GetTutorProfileByIdRequest): Promise<GetTutorProfileByIdResponse> => {
    const query = JSON.stringify(request);
    const axiosResponse = await axios.get<GetTutorProfileByIdResponse>(`/catalog/GetTutorProfileById?query=${query}`);

    return axiosResponse.data;
};

export default getTutorProfileById;
