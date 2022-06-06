import { axios } from "utils/axios";
import { Lesson } from "../types";

export interface GetScheduledLessonsForTutorResponse {
    scheduledLessons: Lesson[];
}

const getScheduledLessonsForTutor = async (): Promise<GetScheduledLessonsForTutorResponse> => {
    const axiosResponse = await axios.get<GetScheduledLessonsForTutorResponse>("/schedule/GetScheduledLessonsForTutor");

    return axiosResponse.data;
};

export default getScheduledLessonsForTutor;
