import { axios } from "utils/axios";

export interface CompleteLessonRequest {
    lessonId: string;
}

const completeLesson = async (request: CompleteLessonRequest): Promise<void> => await axios.post("/schedule/CompleteLesson", request);

export default completeLesson;
