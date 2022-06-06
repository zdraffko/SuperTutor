import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getScheduledLessonsForTutor, { GetScheduledLessonsForTutorResponse } from "../api/getScheduledLessonsForTutor";

const useGetScheduledLessonsForTutor = () => {
    const query = useQuery<GetScheduledLessonsForTutorResponse, AxiosError<string>>("schedule-getScheduledLessonsForTutor", getScheduledLessonsForTutor, {
        refetchOnWindowFocus: false,
        staleTime: 5000
    });

    return {
        scheduledLessonsForTutor: query.data?.scheduledLessons,
        isGetScheduledLessonsForTutorLoading: query.isFetching,
        isGetScheduledLessonsForTutorSuccessful: query.isSuccess,
        isGetScheduledLessonsForTutorFailed: query.isError,
        getScheduledLessonsForTutorErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetScheduledLessonsForTutor;
