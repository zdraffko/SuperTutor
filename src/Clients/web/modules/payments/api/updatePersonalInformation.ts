import { axios } from "utils/axios";

export interface UpdatePersonalInformationRequest {
    dateOfBirth: string;
}

const updatePersonalInformation = async (request: UpdatePersonalInformationRequest): Promise<void> => await axios.post("/payments/UpdateTutorPersonalInformation", request);

export default updatePersonalInformation;
