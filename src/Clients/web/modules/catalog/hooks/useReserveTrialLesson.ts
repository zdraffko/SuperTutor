import { AxiosError } from "axios";
import { useMutation } from "react-query";
import reserveTrialLesson, { ReserveTrialLessonRequest, ReserveTrialLessonResponse } from "../api/reserveTrialLesson";

const useReserveTrialLesson = () => {
    const mutation = useMutation<ReserveTrialLessonResponse, AxiosError<string>, ReserveTrialLessonRequest, unknown>(reserveTrialLesson);

    return {
        reserveTrialLesson: mutation.mutateAsync,
        isReserveTrialLessonLoading: mutation.isLoading,
        isReserveTrialLessonSuccessful: mutation.isSuccess,
        isReserveTrialLessonFailed: mutation.isError,
        reserveTrialLessonErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetReserveTrialLessonRequestState: mutation.reset
    };
};

export default useReserveTrialLesson;
