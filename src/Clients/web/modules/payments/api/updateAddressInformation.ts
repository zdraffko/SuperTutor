import { axios } from "utils/axios";

export interface UpdateAddressInformationRequest {
    tutorId: string;
    state: string;
    city: string;
    addressLineOne: string;
    addressLineTwo: string;
    postalCode: string;
}

const updateAddressInformation = async (request: UpdateAddressInformationRequest): Promise<void> => await axios.post("/payments/UpdateTutorAddressInformation", request);

export default updateAddressInformation;
