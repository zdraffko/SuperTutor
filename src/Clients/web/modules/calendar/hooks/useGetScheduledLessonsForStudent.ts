import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getScheduledLessonsForStudent, { GetScheduledLessonsForStudentResponse } from "../api/getScheduledLessonsForStudent";

const useGetScheduledLessonsForStudent = () => {
    const query = useQuery<GetScheduledLessonsForStudentResponse, AxiosError<string>>("schedule-getScheduledLessonsForStudent", getScheduledLessonsForStudent, {
        refetchOnWindowFocus: false,
        staleTime: 5000
    });

    return {
        scheduledLessonsForStudent: query.data?.scheduledLessons,
        isGetScheduledLessonsForStudentLoading: query.isFetching,
        isGetScheduledLessonsForStudentSuccessful: query.isSuccess,
        isGetScheduledLessonsForStudentFailed: query.isError,
        getScheduledLessonsForStudentErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetScheduledLessonsForStudent;
