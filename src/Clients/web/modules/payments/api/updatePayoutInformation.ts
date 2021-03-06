import { axios } from "utils/axios";

export interface UpdatePayoutInformationRequest {
    bankAccountHolderFullName: string;
    bankAccountHolderType: string;
    bankAccountIban: string;
}

const updatePayoutInformation = async (request: UpdatePayoutInformationRequest): Promise<void> => await axios.post("/payments/UpdateTutorPayoutInformation", request);

export default updatePayoutInformation;
