import { axios } from "utils/axios";

export interface AddAvailabilityRequest {
    date: string;
    startTime: string;
}

const addAvailability = async (request: AddAvailabilityRequest): Promise<void> => await axios.post("/schedule/AddTutorAvailability", request);

export default addAvailability;
