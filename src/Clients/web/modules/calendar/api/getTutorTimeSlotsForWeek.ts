import { axios } from "utils/axios";
import { TimeSlot } from "../types";

export interface GetTutorTimeSlotsForWeekRequest {
    weekStartDate: string;
}

export interface GetTutorTimeSlotsForWeekRequestResponse {
    timeSlotsForWeek: TimeSlot[];
}

const getTutorTimeSlotsForWeek = async (request: GetTutorTimeSlotsForWeekRequest): Promise<GetTutorTimeSlotsForWeekRequestResponse> => {
    const query = JSON.stringify(request);
    const axiosResponse = await axios.get<GetTutorTimeSlotsForWeekRequestResponse>(`/schedule/GetTutorTimeSlotsForWeek?query=${query}`);

    return axiosResponse.data;
};

export default getTutorTimeSlotsForWeek;
