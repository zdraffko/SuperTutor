import { axios } from "utils/axios";

export interface UpdatePersonalInformationRequest {
    tutorId: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
}

const updatePersonalInformation = async (request: UpdatePersonalInformationRequest): Promise<void> => await axios.post("/payments/UpdateTutorPersonalInformation", request);

export default updatePersonalInformation;
