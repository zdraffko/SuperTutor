import { axios } from "utils/axios";

export interface ReserveTrialLessonRequest {
    tutorId: string;
    date: string;
    startTime: string;
    subject: string;
    grade: string;
}

export interface ReserveTrialLessonResponse {
    lessonId: string;
}

const reserveTrialLesson = async (request: ReserveTrialLessonRequest): Promise<ReserveTrialLessonResponse> => {
    const axiosResponse = await axios.post<ReserveTrialLessonResponse>("/catalog/ReserveTrialLesson", request);

    return axiosResponse.data;
};

export default reserveTrialLesson;
