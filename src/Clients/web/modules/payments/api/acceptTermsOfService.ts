import { axios } from "utils/axios";

const acceptTermsOfService = async (): Promise<void> => await axios.post("/payments/AcceptTutorTermsOfService");

export default acceptTermsOfService;
