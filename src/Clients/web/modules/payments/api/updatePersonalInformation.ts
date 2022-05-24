import { axios } from "utils/axios";

export interface UpdatePersonalInformationRequest {
    firstName: string;
    lastName: string;
    dateOfBirth: string;
}

const updatePersonalInformation = async (request: UpdatePersonalInformationRequest): Promise<void> =>
    await axios.post("/payments/UpdateAccountPersonalInformation", { ...request, connectedAccountId: "", connectedPersonId: "" });

export default updatePersonalInformation;
