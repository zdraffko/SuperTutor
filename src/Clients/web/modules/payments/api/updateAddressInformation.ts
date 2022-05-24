import { axios } from "utils/axios";

export interface UpdateAddressInformationRequest {
    state: string;
    city: string;
    addressLineOne: string;
    addressLineTwo: string;
    postalCode: string;
}

const updateAddressInformation = async (request: UpdateAddressInformationRequest): Promise<void> =>
    await axios.post("/payments/UpdateAccountAddressInformation", { ...request, connectedAccountId: "", connectedPersonId: "" });

export default updateAddressInformation;
