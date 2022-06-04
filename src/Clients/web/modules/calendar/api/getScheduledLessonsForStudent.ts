import { axios } from "utils/axios";
import { Lesson } from "../types";

export interface GetScheduledLessonsForStudentResponse {
    scheduledLessons: Lesson[];
}

const getScheduledLessonsForStudent = async (): Promise<GetScheduledLessonsForStudentResponse> => {
    const axiosResponse = await axios.get<GetScheduledLessonsForStudentResponse>("/schedule/GetScheduledLessonsForStudent");

    return axiosResponse.data;
};

export default getScheduledLessonsForStudent;
